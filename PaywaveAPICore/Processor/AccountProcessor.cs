using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using PaywaveAPICore.Extension;
using PaywaveAPICore.Response;
using PaywaveAPICore.Utilities;
using PaywaveAPIData.DataService.Interface;
using PaywaveAPIData.DTO;
using PaywaveAPIData.Enum;
using PaywaveAPIData.Model;
using PaywaveAPIData.Response;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaywaveAPICore.Processor
{
    public class AccountProcessor
    {

        private readonly IAccountDataService _accountDataService;
        private readonly IAutheticationDataService _autheticationDataService;
        private readonly ICardDataService _cardDataService;
        private readonly IActionContextAccessor _ip;
        private readonly ITransactionDataService _transactionDataService;


        public AccountProcessor(IAccountDataService accountDataService,ICardDataService cardDataService, IAutheticationDataService autheticationDataService,
            IActionContextAccessor ip, ITransactionDataService transactionDataService)
        {
            _accountDataService = accountDataService;
            _cardDataService = cardDataService;
            _autheticationDataService = autheticationDataService;
            _ip = ip;
            _transactionDataService = transactionDataService;
        }

        public ServiceResponse<Account> CreateAccount(string clientId, string transactionPin)
        {
            ServiceResponse<Account> resp = new();
            if (Equals(transactionPin.Length, 6) == false)
            {
                resp.message = "Transaction Pin must be 6 digit pin";
                resp.statusCode = ResponseStatus.BAD_REQUEST;
                return resp;
            }
            //check if client already have an account created
            Account clientAccount = _accountDataService.GetbyClientId(clientId);
            if(clientAccount is not null)
            {
                resp.message = "Account already exist!";
                resp.statusCode = ResponseStatus.CONFLICT;
                resp.data = clientAccount;
                return resp;
            }
            Client client = _autheticationDataService.GetClientById(clientId);
            if(client == null)
            {
                //client does not exist
                resp.message = "Client not Found";
                resp.statusCode = ResponseStatus.NOT_FOUND;
                return resp;
            }
            Account account = CreateAccount(client, transactionPin);
            _accountDataService.CreateAccount(account);

            Card card = GenerateCard(account);
            card.AccountID = account.ID;
            _cardDataService.CreateCard(card);
            resp.message = "Account and Card successfully Created";
            resp.statusCode = ResponseStatus.OK;
            resp.data = account;
            return resp;
        }

        public ServiceResponse<Card> GetCardDetails(string accountNumber)
        {
            ServiceResponse<Card> resp = new();
            var card = _cardDataService.GetCardbyAccountNo(accountNumber);
            if(card is null)
            {
                resp.message = "Incorrect Account Number";
                resp.statusCode = ResponseStatus.NOT_FOUND;
                return resp;
            }
            resp.data = card;
            resp.message = "Card Details";
            return resp;
        }

        public ServiceResponse<Account> GetAccountdetails(string accountNumber)
        {
            ServiceResponse<Account> resp = new();
            var account = _accountDataService.GetbyAccountNo(accountNumber);
            if (account is null)
            {
                resp.message = "Incorrect Account Number";
                resp.statusCode = ResponseStatus.NOT_FOUND;
                return resp;
            }
            resp.data = account;
            resp.message = "Account Details";
            return resp;
        }

        public ServiceResponse<Account> GetAccountdetailsByClientId(string clientId)
        {
            ServiceResponse<Account> resp = new();
            var account = _accountDataService.GetbyClientId(clientId);
            if (account is null)
            {
                resp.message = "Account Has not been Created For this Client";
                resp.statusCode = ResponseStatus.NOT_FOUND;
                return resp;
            }
            resp.data = account;
            resp.message = "Account Details";
            return resp;
        }


        public ServiceResponse<CardTransactionResponse> IntiateCardTransaction(InitiateCardRequestDTO request, string merchantAccountNumber)
        {
            var remoteIp = $"Remote IP of Transaction {_ip?.ActionContext?.HttpContext?.Connection?.RemoteIpAddress}";
            ServiceResponse<CardTransactionResponse> resp = new();
            //check if the merchant account number exist to start a credit
            Account merchantAccount = _accountDataService.GetbyAccountNo(merchantAccountNumber);
            if (merchantAccount is null)
            {
                resp.message = "Merchant Account Number is Incorrect";
                resp.statusCode = ResponseStatus.NOT_FOUND;
                return resp;
            }

            //check if the card is valid
            Card card = _cardDataService.GetCardbyCardNumber(request.CardNumber);
            if(card is null)
            {
                resp.message = "Card does not Exist";
                resp.statusCode = ResponseStatus.NOT_FOUND;
                return resp;
            }
            Account sourceAccount = _accountDataService.GetbyAccountNo(card.AccountNo);
            if(sourceAccount is null)
            {
                resp.message = "Account does not Exist";
                resp.statusCode = ResponseStatus.NOT_FOUND;
                return resp;
            }
            //check if the account has enough money to make transaction
            if (sourceAccount.AvailableBalance < request.Amount)
            {
                resp.message = "Insufficient Fund";
                resp.statusCode = ResponseStatus.UNPROCESSABLE;
                return resp;
            }
            //check if the transaction pin is valid for the transaction
            if (PasswordHasher.Verify(request.TransactionPin, sourceAccount.TransactionPin) is false)
            {
                resp.message = "Invalid Transaction Pin";
                resp.statusCode = ResponseStatus.UNAUTHORIZED;
                return resp;
            }
            //start debit and credit process
            CreditandDebitProcess(sourceAccount, merchantAccount, request.Amount);

            //Create Transaction Information

            CardTransactionResponse transactResponse = new CardTransactionResponse()
            {
                Amount = request.Amount,
                BeneficiaryAccountNumber = merchantAccount.AccountNumber,
                BeneficiaryName = merchantAccount.AccountName,
                SourceAccountClientId = sourceAccount.ClientID,
                SourceAccountName = sourceAccount.AccountName,
                SourceAccountNumber = sourceAccount.AccountNumber,
                Narration = request.Narration,
                TransactionReference = Guid.NewGuid().ToString(),
            };
            //log transaction to db
            LogTransaction(transactResponse, remoteIp);
            resp.data = transactResponse;
            resp.message = "Transaction Completed";
            return resp;
        }
        
        public ServiceResponse<IEnumerable<Transaction>> GetAllTransaction(string accountNumber)
        {
            ServiceResponse<IEnumerable<Transaction>> resp = new();
            var data = _transactionDataService.GetbyAccountNo(accountNumber);
            if(data is null )
            {
                resp.message = "No Transaction Record";
                resp.statusCode = ResponseStatus.UNPROCESSABLE;
                return resp;
            }
            resp.data = data;
            resp.message = $"All Transaction Completed is {data.Count()}";
            return resp;
        }
        private Account CreateAccount(Client client, string transactionPin)
        {
            return new()
            {
                AccountName = client.lastName + " " + client.firstName + " " + client.middleName,
                AccountNumber = "30" + GenerateStringExtension.GenerateRandomNumber(8).ToString(),
                AvailableBalance = 10000000,
                ClientID = client.ID,
                LastRefreshed = DateTime.Now,
                PendingBalance = 10000000, 
                TransactionPin = PasswordHasher.Hash(transactionPin)
            };
        }

        private Card GenerateCard(Account account)
        {
            return new Card()
            {
                AccountID = account.ID,
                AccountNo = account.AccountNumber,
                CardNo = "6499" + GenerateStringExtension.GenerateRandomNumber(12).ToString(),
                cardType = CardType.PaywaveCoreStaff,
                ExpirationDate = DateTime.Now.AddYears(3),
            };
        }

        private void CreditandDebitProcess(Account sourceAccount, Account beneficiaryAccount, int Amount)
        {
            sourceAccount.AvailableBalance -= Amount;
            beneficiaryAccount.AvailableBalance += Amount;
            beneficiaryAccount.LastRefreshed = DateTime.Now;
            sourceAccount.LastRefreshed = DateTime.Now;
            sourceAccount.PendingBalance = sourceAccount.AvailableBalance;
            beneficiaryAccount.PendingBalance = beneficiaryAccount.AvailableBalance;
            _accountDataService.UpdateAccount(sourceAccount.ID, sourceAccount);
            _accountDataService.UpdateAccount(beneficiaryAccount.ID, beneficiaryAccount);
        }
        private void LogTransaction(CardTransactionResponse request, string requestIp )
        {
            Transaction transact = new Transaction()
            {
                Amount = request.Amount,
                BeneficiaryAccountNumber = request.BeneficiaryAccountNumber,
                BeneficiaryName = request.BeneficiaryName,
                SourceAccountClientId = request.SourceAccountClientId,
                SourceAccountName = request.SourceAccountName,
                SourceAccountNumber = request.SourceAccountNumber,
                Narration = request.Narration,
                TransactionReference = request.TransactionReference,
                TransactionDate = DateTime.Now,
                TransactionIp = requestIp,
            };
            _transactionDataService.InsertTransaction(transact);
        }
    }
}

