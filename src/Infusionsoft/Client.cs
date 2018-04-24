using System;
using System.Net.Http;
using System.Threading.Tasks;
using Infusion.Model;
using LanguageExt;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable RedundantStringInterpolation
namespace Infusion
{
    using static Prelude;
    using static JsonConvert;

    public class InfusionClient
    {
        private readonly HttpClient _client;
        private readonly InfusionConfig _config;

        public InfusionClient(HttpClient client, InfusionConfig config)
        {
            _client = client;
            _config = config;
        }

        /// <summary>
        /// Retrieve account profile
        /// </summary>
        public Task<AccountProfile> GetAccountProfile()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/account/profile")
                {Content = MakeHttpContent()};
            return default(AccountProfile).AsTask();
        }

        /// <summary>
        /// Retrieve Commissions
        /// </summary>
        /// <param name = "affiliateId">Affiliate to retrieve commissions for</param>
        /// <param name = "offset">Sets a beginning range of items to return</param>
        /// <param name = "limit">Sets a total of items to return</param>
        /// <param name = "until">Date to search to ex. `2017-01-01T22:17:59.039Z`</param>
        /// <param name = "since">Date to start searching from ex. `2017-01-01T22:17:59.039Z`</param>
        public Task<AffiliateCommissionList> SearchCommissions(long affiliateId = default, int offset = default,
            int limit = default, string until = default, string since = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/affiliates/commissions")
            {
                Content = MakeHttpContent(ContentProperty("affiliateId", affiliateId),
                    ContentProperty("offset", offset), ContentProperty("limit", limit), ContentProperty("until", until),
                    ContentProperty("since", since))
            };
            return default(AffiliateCommissionList).AsTask();
        }

        /// <summary>
        /// Retrieve Affiliate Model
        /// </summary>
        public Task<ObjectModel> RetrieveAffiliateModel()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/affiliates/model")
                {Content = MakeHttpContent()};
            return default(ObjectModel).AsTask();
        }

        /// <summary>
        /// List Appointments
        /// </summary>
        /// <param name = "contactId">Optionally find appointments with a contact</param>
        /// <param name = "offset">Sets a beginning range of items to return</param>
        /// <param name = "limit">Sets a total of items to return</param>
        /// <param name = "until">Date to search to ex. `2017-01-01T22:17:59.039Z`</param>
        /// <param name = "since">Date to start searching from ex. `2017-01-01T22:17:59.039Z`</param>
        public Task<AppointmentList> ListAppointments(long contactId = default, int offset = default,
            int limit = default, string until = default, string since = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/appointments")
            {
                Content = MakeHttpContent(ContentProperty("contactId", contactId), ContentProperty("offset", offset),
                    ContentProperty("limit", limit), ContentProperty("until", until), ContentProperty("since", since))
            };
            return default(AppointmentList).AsTask();
        }

        /// <summary>
        /// Retrieve Appointment Model
        /// </summary>
        public Task<ObjectModel> RetrieveAppointmentModel()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/appointments/model")
                {Content = MakeHttpContent()};
            return default(ObjectModel).AsTask();
        }

        /// <summary>
        /// Retrieve an Appointment
        /// </summary>
        /// <param name = "appointmentId">appointmentId</param>
        public Task<Appointment> GetAppointment(long appointmentId)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/appointments/{appointmentId}")
                {Content = MakeHttpContent(ContentProperty("appointmentId", appointmentId))};
            return default(Appointment).AsTask();
        }

        /// <summary>
        /// List Campaigns
        /// </summary>
        /// <param name = "orderDirection">How to order the data i.e. ascending (A-Z) or descending (Z-A)</param>
        /// <param name = "order">Attribute to order items by</param>
        /// <param name = "searchText">Optional text to search</param>
        /// <param name = "offset">Sets a beginning range of items to return</param>
        /// <param name = "limit">Sets a total of items to return</param>
        public Task<CampaignList> ListCampaigns(string orderDirection = default, string order = default,
            string searchText = default, int offset = default, int limit = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/campaigns")
            {
                Content = MakeHttpContent(ContentProperty("orderDirection", orderDirection),
                    ContentProperty("order", order), ContentProperty("searchText", searchText),
                    ContentProperty("offset", offset), ContentProperty("limit", limit))
            };
            return default(CampaignList).AsTask();
        }

        /// <summary>
        /// Retrieve a Campaign
        /// </summary>
        /// <param name = "campaignId">campaignId</param>
        /// <param name = "optionalProperties">Comma-delimited list of Campaign properties to include in the response. (The fields `goals` and `sequences` aren't included, by default.)</param>
        public Task<Campaign> GetCampaign(long campaignId, Lst<string> optionalProperties = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/campaigns/{campaignId}")
            {
                Content = MakeHttpContent(ContentProperty("campaignId", campaignId),
                    ContentProperty("optionalProperties", optionalProperties))
            };
            return default(Campaign).AsTask();
        }

        /// <summary>
        /// Add Multiple to Campaign Sequence
        /// </summary>
        /// <param name = "ids">ids</param>
        /// <param name = "sequenceId">sequenceId</param>
        /// <param name = "campaignId">campaignId</param>
        public Task<Unit> AddContactsToCampaignSequence(Model.SetOfIds ids, long sequenceId, long campaignId)
        {
            var message =
                new HttpRequestMessage(HttpMethod.Post, $"/campaigns/{campaignId}/sequences/{sequenceId}/contacts")
                {
                    Content = MakeHttpContent(ContentProperty("ids", ids), ContentProperty("sequenceId", sequenceId),
                        ContentProperty("campaignId", campaignId))
                };
            return default(Unit).AsTask();
        }

        /// <summary>
        /// Add to Campaign Sequence
        /// </summary>
        /// <param name = "contactId">contactId</param>
        /// <param name = "sequenceId">sequenceId</param>
        /// <param name = "campaignId">campaignId</param>
        public Task<Unit> AddContactToCampaignSequence(long contactId, long sequenceId, long campaignId)
        {
            var message = new HttpRequestMessage(HttpMethod.Post,
                $"/campaigns/{campaignId}/sequences/{sequenceId}/contacts/{contactId}")
            {
                Content = MakeHttpContent(ContentProperty("contactId", contactId),
                    ContentProperty("sequenceId", sequenceId), ContentProperty("campaignId", campaignId))
            };
            return default(Unit).AsTask();
        }

        /// <summary>
        /// List Companies
        /// </summary>
        /// <param name = "optionalProperties">Comma-delimited list of Company properties to include in the response. (Fields such as `notes`, `fax_number` and `custom_fields` aren't included, by default.)</param>
        /// <param name = "orderDirection">How to order the data i.e. ascending (A-Z) or descending (Z-A)</param>
        /// <param name = "order">Attribute to order items by</param>
        /// <param name = "companyName">Optional company name to query on</param>
        /// <param name = "offset">Sets a beginning range of items to return</param>
        /// <param name = "limit">Sets a total of items to return</param>
        public Task<CompanyList> ListCompanies(Lst<string> optionalProperties = default,
            string orderDirection = default, string order = default, string companyName = default, int offset = default,
            int limit = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/companies")
            {
                Content = MakeHttpContent(ContentProperty("optionalProperties", optionalProperties),
                    ContentProperty("orderDirection", orderDirection), ContentProperty("order", order),
                    ContentProperty("companyName", companyName), ContentProperty("offset", offset),
                    ContentProperty("limit", limit))
            };
            return default(CompanyList).AsTask();
        }

        /// <summary>
        /// Retrieve Company Model
        /// </summary>
        public Task<ObjectModel> RetrieveCompanyModel()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/companies/model")
                {Content = MakeHttpContent()};
            return default(ObjectModel).AsTask();
        }

        /// <summary>
        /// List Contacts
        /// </summary>
        /// <param name = "orderDirection">How to order the data i.e. ascending (A-Z) or descending (Z-A)</param>
        /// <param name = "order">Attribute to order items by</param>
        /// <param name = "familyName">Optional last name or surname to query on</param>
        /// <param name = "givenName">Optional first name or forename to query on</param>
        /// <param name = "email">Optional email address to query on</param>
        /// <param name = "offset">Sets a beginning range of items to return</param>
        /// <param name = "limit">Sets a total of items to return</param>
        public Task<ContactList> ListContacts(string orderDirection = default, string order = default,
            string familyName = default, string givenName = default, string email = default, int offset = default,
            int limit = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/contacts")
            {
                Content = MakeHttpContent(ContentProperty("orderDirection", orderDirection),
                    ContentProperty("order", order), ContentProperty("familyName", familyName),
                    ContentProperty("givenName", givenName), ContentProperty("email", email),
                    ContentProperty("offset", offset), ContentProperty("limit", limit))
            };
            return default(ContactList).AsTask();
        }

        /// <summary>
        /// Retrieve Contact Model
        /// </summary>
        public Task<ObjectModel> RetrieveContactModel()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/contacts/model")
                {Content = MakeHttpContent()};
            return default(ObjectModel).AsTask();
        }

        /// <summary>
        /// Delete a Contact
        /// </summary>
        /// <param name = "contactId">contactId</param>
        public Task<Unit> DeleteContact(long contactId)
        {
            var message = new HttpRequestMessage(HttpMethod.Delete, $"/contacts/{contactId}")
                {Content = MakeHttpContent(ContentProperty("contactId", contactId))};
            return default(Unit).AsTask();
        }

        /// <summary>
        /// Create a Credit Card
        /// </summary>
        /// <param name = "contactId">contactId</param>
        /// <param name = "creditCard">creditCard</param>
        public Task<CreditCardAdded> CreateCreditCard(long contactId, Model.CreditCard creditCard = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, $"/contacts/{contactId}/creditCards")
            {
                Content = MakeHttpContent(ContentProperty("contactId", contactId),
                    ContentProperty("creditCard", creditCard))
            };
            return default(CreditCardAdded).AsTask();
        }

        /// <summary>
        /// List Emails
        /// </summary>
        /// <param name = "contactId">contactId</param>
        /// <param name = "email">Optional email address to query on</param>
        /// <param name = "contactId2">Optional Contact Id to find Emails for</param>
        /// <param name = "offset">Sets a beginning range of items to return</param>
        /// <param name = "limit">Sets a total of items to return</param>
        public Task<EmailSentQueryResultList> ListEmailsForContact(long contactId, string email = default,
            long contactId2 = default, int offset = default, int limit = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/contacts/{contactId}/emails")
            {
                Content = MakeHttpContent(ContentProperty("contactId", contactId), ContentProperty("email", email),
                    ContentProperty("contactId2", contactId2), ContentProperty("offset", offset),
                    ContentProperty("limit", limit))
            };
            return default(EmailSentQueryResultList).AsTask();
        }

        /// <summary>
        /// List Applied Tags
        /// </summary>
        /// <param name = "contactId">contactId</param>
        /// <param name = "offset">Sets a beginning range of items to return</param>
        /// <param name = "limit">Sets a total of items to return</param>
        public Task<ContactTagList> ListAppliedTags(long contactId, int offset = default, int limit = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/contacts/{contactId}/tags")
            {
                Content = MakeHttpContent(ContentProperty("contactId", contactId), ContentProperty("offset", offset),
                    ContentProperty("limit", limit))
            };
            return default(ContactTagList).AsTask();
        }

        /// <summary>
        /// Remove Applied Tag
        /// </summary>
        /// <param name = "tagId">tagId</param>
        /// <param name = "contactId">contactId</param>
        public Task<Unit> RemoveTagsFromContact(long tagId, long contactId)
        {
            var message = new HttpRequestMessage(HttpMethod.Delete, $"/contacts/{contactId}/tags/{tagId}")
                {Content = MakeHttpContent(ContentProperty("tagId", tagId), ContentProperty("contactId", contactId))};
            return default(Unit).AsTask();
        }

        /// <summary>
        /// Retrieve a Contact
        /// </summary>
        /// <param name = "id">id</param>
        /// <param name = "optionalProperties">Comma-delimited list of Contact properties to include in the response. (Some fields such as `lead_source_id`, `custom_fields`, and `job_title` aren't included, by default.)</param>
        public Task<FullContact> GetContact(long id, Lst<string> optionalProperties = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/contacts/{id}")
            {
                Content = MakeHttpContent(ContentProperty("id", id),
                    ContentProperty("optionalProperties", optionalProperties))
            };
            return default(FullContact).AsTask();
        }

        /// <summary>
        /// List Emails
        /// </summary>
        /// <param name = "email">Optional email address to query on</param>
        /// <param name = "contactId">Optional Contact Id to find Emails for</param>
        /// <param name = "offset">Sets a beginning range of items to return</param>
        /// <param name = "limit">Sets a total of items to return</param>
        public Task<EmailSentQueryResultList> ListEmails(string email = default, long contactId = default,
            int offset = default, int limit = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/emails")
            {
                Content = MakeHttpContent(ContentProperty("email", email), ContentProperty("contactId", contactId),
                    ContentProperty("offset", offset), ContentProperty("limit", limit))
            };
            return default(EmailSentQueryResultList).AsTask();
        }

        /// <summary>
        /// Create a set of Email Records
        /// </summary>
        /// <param name = "emailWithContent">Email records to persist, with content.</param>
        public Task<EmailSentCreateList> CreateEmails(Model.EmailSentCreateList emailWithContent = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, $"/emails/sync")
                {Content = MakeHttpContent(ContentProperty("emailWithContent", emailWithContent))};
            return default(EmailSentCreateList).AsTask();
        }

        /// <summary>
        /// Un-sync a batch of Email Records
        /// </summary>
        /// <param name = "emailIds">emailIds</param>
        public Task<Unit> DeleteEmails(Model.SetOfIds emailIds)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, $"/emails/unsync")
                {Content = MakeHttpContent(ContentProperty("emailIds", emailIds))};
            return default(Unit).AsTask();
        }

        /// <summary>
        /// Retrieve an Email
        /// </summary>
        /// <param name = "id">id</param>
        public Task<EmailSentQueryResultWithContent> GetEmail(long id)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/emails/{id}")
                {Content = MakeHttpContent(ContentProperty("id", id))};
            return default(EmailSentQueryResultWithContent).AsTask();
        }

        /// <summary>
        /// List Files
        /// </summary>
        /// <param name = "name">Filter files based on name, with '*' preceding or following to indicate LIKE queries.</param>
        /// <param name = "type">Filter based on the type of file.</param>
        /// <param name = "permission">Filter based on the permission of files (USER or COMPANY), defaults to BOTH.</param>
        /// <param name = "viewable">Include public or private files in response (PUBLIC or PRIVATE), defaults to BOTH.</param>
        /// <param name = "offset">Sets a beginning range of items to return</param>
        /// <param name = "limit">Sets a total of items to return</param>
        public Task<FileList> ListFiles(string name = default, string type = default, string permission = default,
            string viewable = default, int offset = default, int limit = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/files")
            {
                Content = MakeHttpContent(ContentProperty("name", name), ContentProperty("type", type),
                    ContentProperty("permission", permission), ContentProperty("viewable", viewable),
                    ContentProperty("offset", offset), ContentProperty("limit", limit))
            };
            return default(FileList).AsTask();
        }

        /// <summary>
        /// Retrieve File
        /// </summary>
        /// <param name = "fileId">fileId</param>
        /// <param name = "optionalProperties">Comma-delimited list of File properties to include in the response. (Some fields such as `file_data` aren't included, by default.)</param>
        public Task<FileInformation> GetFile(long fileId, Lst<string> optionalProperties = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/files/{fileId}")
            {
                Content = MakeHttpContent(ContentProperty("fileId", fileId),
                    ContentProperty("optionalProperties", optionalProperties))
            };
            return default(FileInformation).AsTask();
        }

        /// <summary>
        /// List Stored Hook Subscriptions
        /// </summary>
        public Task<Unit> ListStoredHookSubscriptions()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/hooks")
                {Content = MakeHttpContent()};
            return default(Unit).AsTask();
        }

        /// <summary>
        /// List Hook Event Types
        /// </summary>
        public Task<Unit> ListHookEventTypes()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/hooks/event_keys")
                {Content = MakeHttpContent()};
            return default(Unit).AsTask();
        }

        /// <summary>
        /// Retrieve a Hook Subscription
        /// </summary>
        /// <param name = "key">key</param>
        public Task<RestHook> RetrieveAHookSubscription(string key)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/hooks/{key}")
                {Content = MakeHttpContent(ContentProperty("key", key))};
            return default(RestHook).AsTask();
        }

        /// <summary>
        /// Verify a Hook Subscription, Delayed
        /// </summary>
        /// <param name = "xHookSecret">X-Hook-Secret</param>
        /// <param name = "key">key</param>
        public Task<RestHook> VerifyAHookSubscriptionDelayed(string xHookSecret, string key)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, $"/hooks/{key}/delayedVerify")
                {Content = MakeHttpContent(ContentProperty("xHookSecret", xHookSecret), ContentProperty("key", key))};
            return default(RestHook).AsTask();
        }

        /// <summary>
        /// Verify a Hook Subscription
        /// </summary>
        /// <param name = "key">key</param>
        public Task<RestHook> VerifyAHookSubscription(string key)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, $"/hooks/{key}/verify")
                {Content = MakeHttpContent(ContentProperty("key", key))};
            return default(RestHook).AsTask();
        }

        /// <summary>
        /// Retrieve User Info
        /// </summary>
        public Task<UserInfoDTO> GetUserInfo()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/oauth/connect/userinfo")
                {Content = MakeHttpContent()};
            return default(UserInfoDTO).AsTask();
        }

        /// <summary>
        /// List Opportunities
        /// </summary>
        /// <param name = "order">Attribute to order items by</param>
        /// <param name = "searchTerm">Returns opportunities that match any of the contact's `given_name`, `family_name`, `company_name`, and `email_addresses` (searches `EMAIL1` only) fields as well as `opportunity_title`</param>
        /// <param name = "stageId">Returns opportunities for the provided stage id</param>
        /// <param name = "userId">Returns opportunities for the provided user id</param>
        /// <param name = "offset">Sets a beginning range of items to return</param>
        /// <param name = "limit">Sets a total of items to return</param>
        public Task<OpportunityList> ListOpportunities(string order = default, string searchTerm = default,
            long stageId = default, long userId = default, int offset = default, int limit = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/opportunities")
            {
                Content = MakeHttpContent(ContentProperty("order", order), ContentProperty("searchTerm", searchTerm),
                    ContentProperty("stageId", stageId), ContentProperty("userId", userId),
                    ContentProperty("offset", offset), ContentProperty("limit", limit))
            };
            return default(OpportunityList).AsTask();
        }

        /// <summary>
        /// Retrieve Opportunity Model
        /// </summary>
        public Task<ObjectModel> RetrieveOpportunityModel()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/opportunities/model")
                {Content = MakeHttpContent()};
            return default(ObjectModel).AsTask();
        }

        /// <summary>
        /// Retrieve an Opportunity
        /// </summary>
        /// <param name = "opportunityId">opportunityId</param>
        /// <param name = "optionalProperties">Comma-delimited list of Opportunity properties to include in the response. (Some fields such as `custom_fields` aren't included, by default.)</param>
        public Task<Opportunity> GetOpportunity(long opportunityId, Lst<string> optionalProperties = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/opportunities/{opportunityId}")
            {
                Content = MakeHttpContent(ContentProperty("opportunityId", opportunityId),
                    ContentProperty("optionalProperties", optionalProperties))
            };
            return default(Opportunity).AsTask();
        }

        /// <summary>
        /// List Opportunity Stage Pipeline
        /// </summary>
        public Task<Unit> ListOpportunityStagePipelines()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/opportunity/stage_pipeline")
                {Content = MakeHttpContent()};
            return default(Unit).AsTask();
        }

        /// <summary>
        /// List Orders
        /// </summary>
        /// <param name = "productId">Returns orders containing the provided product id</param>
        /// <param name = "contactId">Returns orders for the provided contact id</param>
        /// <param name = "order">Attribute to order items by</param>
        /// <param name = "paid">Sets paid status of items to return</param>
        /// <param name = "offset">Sets a beginning range of items to return</param>
        /// <param name = "limit">Sets a total of items to return</param>
        /// <param name = "until">Date to search to ex. `2017-01-01T22:17:59.039Z`</param>
        /// <param name = "since">Date to start searching from ex. `2017-01-01T22:17:59.039Z`</param>
        public Task<OrderList> ListOrders(long productId = default, long contactId = default, string order = default,
            bool paid = default, int offset = default, int limit = default, string until = default,
            string since = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/orders")
            {
                Content = MakeHttpContent(ContentProperty("productId", productId),
                    ContentProperty("contactId", contactId), ContentProperty("order", order),
                    ContentProperty("paid", paid), ContentProperty("offset", offset), ContentProperty("limit", limit),
                    ContentProperty("until", until), ContentProperty("since", since))
            };
            return default(OrderList).AsTask();
        }

        /// <summary>
        /// Retrieve Custom Order Model
        /// </summary>
        public Task<ObjectModel> RetrieveOrderModel()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/orders/model")
                {Content = MakeHttpContent()};
            return default(ObjectModel).AsTask();
        }

        /// <summary>
        /// Retrieve an Order
        /// </summary>
        /// <param name = "orderId">orderId</param>
        public Task<Order> GetOrder(long orderId)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/orders/{orderId}")
                {Content = MakeHttpContent(ContentProperty("orderId", orderId))};
            return default(Order).AsTask();
        }

        /// <summary>
        /// Retrieve Order Transactions
        /// </summary>
        /// <param name = "orderId">orderId</param>
        /// <param name = "contactId">Returns transactions for the provided contact id</param>
        /// <param name = "offset">Sets a beginning range of items to return</param>
        /// <param name = "limit">Sets a total of items to return</param>
        /// <param name = "until">Date to search to ex. `2017-01-01T22:17:59.039Z`</param>
        /// <param name = "since">Date to start searching from ex. `2017-01-01T22:17:59.039Z`</param>
        public Task<TransactionList> ListTransactionsForOrder(long orderId, long contactId = default,
            int offset = default, int limit = default, string until = default, string since = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/orders/{orderId}/transactions")
            {
                Content = MakeHttpContent(ContentProperty("orderId", orderId), ContentProperty("contactId", contactId),
                    ContentProperty("offset", offset), ContentProperty("limit", limit), ContentProperty("until", until),
                    ContentProperty("since", since))
            };
            return default(TransactionList).AsTask();
        }

        /// <summary>
        /// List Products
        /// </summary>
        /// <param name = "active">Sets status of items to return</param>
        /// <param name = "offset">Sets a beginning range of items to return</param>
        /// <param name = "limit">Sets a total of items to return</param>
        public Task<ProductList> ListProducts(bool active = default, int offset = default, int limit = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/products")
            {
                Content = MakeHttpContent(ContentProperty("active", active), ContentProperty("offset", offset),
                    ContentProperty("limit", limit))
            };
            return default(ProductList).AsTask();
        }

        /// <summary>
        /// Retrieve Synced Products
        /// </summary>
        /// <param name = "offset">Sets a beginning range of items to return</param>
        /// <param name = "limit">Sets a total of items to return</param>
        /// <param name = "syncToken">sync_token</param>
        public Task<ProductStatusList> ListProductsFromSyncToken(int offset = default, int limit = default,
            string syncToken = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/products/sync")
            {
                Content = MakeHttpContent(ContentProperty("offset", offset), ContentProperty("limit", limit),
                    ContentProperty("syncToken", syncToken))
            };
            return default(ProductStatusList).AsTask();
        }

        /// <summary>
        /// Retrieve a Product
        /// </summary>
        /// <param name = "productId">productId</param>
        public Task<Product> GetProduct(long productId)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/products/{productId}")
                {Content = MakeHttpContent(ContentProperty("productId", productId))};
            return default(Product).AsTask();
        }

        /// <summary>
        /// Retrieve application status
        /// </summary>
        public Task<Setting> GetApplicationEnabled()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/setting/application/enabled")
                {Content = MakeHttpContent()};
            return default(Setting).AsTask();
        }

        /// <summary>
        /// List Contact types
        /// </summary>
        public Task<Setting> GetContactOptionTypes()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/setting/contact/optionTypes")
                {Content = MakeHttpContent()};
            return default(Setting).AsTask();
        }

        /// <summary>
        /// Retrieve Subscription Model
        /// </summary>
        public Task<ObjectModel> RetrieveSubscriptionModel()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/subscriptions/model")
                {Content = MakeHttpContent()};
            return default(ObjectModel).AsTask();
        }

        /// <summary>
        /// List Tags
        /// </summary>
        /// <param name = "category">Category Id of tags to filter by</param>
        /// <param name = "offset">Sets a beginning range of items to return</param>
        /// <param name = "limit">Sets a total of items to return</param>
        public Task<Tags> ListTags(long category = default, int offset = default, int limit = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/tags")
            {
                Content = MakeHttpContent(ContentProperty("category", category), ContentProperty("offset", offset),
                    ContentProperty("limit", limit))
            };
            return default(Tags).AsTask();
        }

        /// <summary>
        /// Create Tag Category
        /// </summary>
        /// <param name = "tagCategory">tagCategory</param>
        public Task<TagCategory> CreateTagCategory(Model.CreateTagCategory tagCategory)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, $"/tags/categories")
                {Content = MakeHttpContent(ContentProperty("tagCategory", tagCategory))};
            return default(TagCategory).AsTask();
        }

        /// <summary>
        /// Retrieve a Tag
        /// </summary>
        /// <param name = "id">id</param>
        public Task<Tag> GetTag(long id)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/tags/{id}")
                {Content = MakeHttpContent(ContentProperty("id", id))};
            return default(Tag).AsTask();
        }

        /// <summary>
        /// List Tagged Contacts
        /// </summary>
        /// <param name = "tagId">tagId</param>
        /// <param name = "offset">Sets a beginning range of items to return</param>
        /// <param name = "limit">Sets a total of items to return</param>
        public Task<TaggedContactList> ListContactsForTagId(long tagId, int offset = default, int limit = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/tags/{tagId}/contacts")
            {
                Content = MakeHttpContent(ContentProperty("tagId", tagId), ContentProperty("offset", offset),
                    ContentProperty("limit", limit))
            };
            return default(TaggedContactList).AsTask();
        }

        /// <summary>
        /// Remove Tag from Contact
        /// </summary>
        /// <param name = "contactId">contactId</param>
        /// <param name = "tagId">tagId</param>
        public Task<Unit> RemoveTagFromContactId(long contactId, long tagId)
        {
            var message = new HttpRequestMessage(HttpMethod.Delete, $"/tags/{tagId}/contacts/{contactId}")
                {Content = MakeHttpContent(ContentProperty("contactId", contactId), ContentProperty("tagId", tagId))};
            return default(Unit).AsTask();
        }

        /// <summary>
        /// List Tasks
        /// </summary>
        /// <param name = "order">Attribute to order items by</param>
        /// <param name = "offset">Sets a beginning range of items to return</param>
        /// <param name = "limit">Sets a total of items to return</param>
        /// <param name = "completed">Sets completed status of items to return</param>
        /// <param name = "until">Date to search to ex. `2017-01-01T22:17:59.039Z`</param>
        /// <param name = "since">Date to start searching from ex. `2017-01-01T22:17:59.039Z`</param>
        /// <param name = "userId">user_id</param>
        /// <param name = "hasDueDate">has_due_date</param>
        /// <param name = "contactId">contact_id</param>
        public Task<TaskList> ListTasks(string order = default, int offset = default, int limit = default,
            bool completed = default, string until = default, string since = default, long userId = default,
            bool hasDueDate = default, long contactId = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/tasks")
            {
                Content = MakeHttpContent(ContentProperty("order", order), ContentProperty("offset", offset),
                    ContentProperty("limit", limit), ContentProperty("completed", completed),
                    ContentProperty("until", until), ContentProperty("since", since), ContentProperty("userId", userId),
                    ContentProperty("hasDueDate", hasDueDate), ContentProperty("contactId", contactId))
            };
            return default(TaskList).AsTask();
        }

        /// <summary>
        /// Retrieve Task Model
        /// </summary>
        public Task<ObjectModel> RetrieveTaskModel()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/tasks/model")
                {Content = MakeHttpContent()};
            return default(ObjectModel).AsTask();
        }

        /// <summary>
        /// Search Tasks
        /// </summary>
        /// <param name = "order">Attribute to order items by</param>
        /// <param name = "offset">Sets a beginning range of items to return</param>
        /// <param name = "limit">Sets a total of items to return</param>
        /// <param name = "completed">Sets completed status of items to return</param>
        /// <param name = "until">Date to search to ex. `2017-01-01T22:17:59.039Z`</param>
        /// <param name = "since">Date to start searching from ex. `2017-01-01T22:17:59.039Z`</param>
        /// <param name = "userId">Returns tasks for the provided user id</param>
        /// <param name = "hasDueDate">Returns tasks that have an 'action date' when set to true</param>
        /// <param name = "contactId">Returns tasks for the provided contact id</param>
        public Task<TaskList> ListTasksForCurrentUser(string order = default, int offset = default, int limit = default,
            bool completed = default, string until = default, string since = default, long userId = default,
            bool hasDueDate = default, long contactId = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/tasks/search")
            {
                Content = MakeHttpContent(ContentProperty("order", order), ContentProperty("offset", offset),
                    ContentProperty("limit", limit), ContentProperty("completed", completed),
                    ContentProperty("until", until), ContentProperty("since", since), ContentProperty("userId", userId),
                    ContentProperty("hasDueDate", hasDueDate), ContentProperty("contactId", contactId))
            };
            return default(TaskList).AsTask();
        }

        /// <summary>
        /// Retrieve a Task
        /// </summary>
        /// <param name = "taskId">taskId</param>
        public Task<InfusionTask> GetTask(string taskId)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/tasks/{taskId}")
                {Content = MakeHttpContent(ContentProperty("taskId", taskId))};
            return default(InfusionTask).AsTask();
        }

        /// <summary>
        /// List Transactions
        /// </summary>
        /// <param name = "contactId">Returns transactions for the provided contact id</param>
        /// <param name = "offset">Sets a beginning range of items to return</param>
        /// <param name = "limit">Sets a total of items to return</param>
        /// <param name = "until">Date to search to ex. `2017-01-01T22:17:59.039Z`</param>
        /// <param name = "since">Date to start searching from ex. `2017-01-01T22:17:59.039Z`</param>
        public Task<TransactionList> ListTransactions(long contactId = default, int offset = default,
            int limit = default, string until = default, string since = default)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/transactions")
            {
                Content = MakeHttpContent(ContentProperty("contactId", contactId), ContentProperty("offset", offset),
                    ContentProperty("limit", limit), ContentProperty("until", until), ContentProperty("since", since))
            };
            return default(TransactionList).AsTask();
        }

        /// <summary>
        /// Retrieve a Transaction
        /// </summary>
        /// <param name = "transactionId">transactionId</param>
        public Task<Transaction> GetTransaction(long transactionId)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"/transactions/{transactionId}")
                {Content = MakeHttpContent(ContentProperty("transactionId", transactionId))};
            return default(Transaction).AsTask();
        }

        static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        static Option<(string name, object value)> ContentProperty(string name, object value) =>
            Optional(value).Map(x => (name, x));

        static HttpContent MakeHttpContent(params Option<(string name, object value)>[] values) => ifNoneUnsafe(
            from map1 in Some(values.FoldT(HashMap<string, object>(), (acc, x) => acc.Add(x.name, x.value)))
            where !map1.IsEmpty
            select new StringContent(SerializeObject(map1, SerializerSettings)), () => null);

        EitherAsync<Error, T> Send<T>(HttpMethod method, string relativeUrl,
            params Option<(string name, object value)>[] values)
        {
            var res = TrySend<T>(
                new HttpRequestMessage(method, $"https://api.infusionsoft.com/crm/rest/v1/{relativeUrl}")
                {
                    Content = MakeHttpContent(values)
                });
        }

        TryAsync<T> TrySend<T>(HttpRequestMessage message) =>
            from response in TryAsync(() => _client.SendAsync(message))
            from result in ReadResult<T>(response)
            select result;

        TryAsync<T> ReadResult<T>(HttpResponseMessage response) =>
            from json in TryAsync(response.Content.ReadAsStringAsync())
            from result in Try(DeserializeObject<T>(json)).ToAsync()
            select result;

        TryAsync<Error> ReadError(HttpResponseMessage response) =>
            from json in TryAsync(response.Content.ReadAsStringAsync())
            from result in Try(DeserializeObject<Error>(json)).ToAsync()
            select result;
    }
}