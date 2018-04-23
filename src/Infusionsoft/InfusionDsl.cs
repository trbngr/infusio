//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v11.17.2.0 (NJsonSchema v9.10.45.0 (Newtonsoft.Json v9.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

using System.Text;
using LanguageExt;
using Newtonsoft.Json;
using Infusion.Client;

// ReSharper disable InconsistentNaming

namespace Infusion
{
    using static Prelude;
    using static JsonConvert;

    public static class InfusionDsl
    {
//        internal static InfusionOp<A> Return<A>(A value) =>
//            new InfusionOp<A>.Return(value);
//
//        internal static InfusionOp<Unit> ReturnUnit() =>
//            new InfusionOp<Unit>.Return(unit);
//
//        public static InfusionOp<AccountProfile> GetAccountProfile() =>
//            new InfusionOp<AccountProfile>.GetAccountProfile(Return);
//
//        public static InfusionOp<AccountProfile> UpdateAccountInfo(AccountProfile accountInfo) =>
//            new InfusionOp<AccountProfile>.UpdateAccountInfo(Return, accountInfo);
//
//        public static InfusionOp<AffiliateCommissionList> SearchCommissions(string since, string until, int? limit, int? offset, long? affiliateId) =>
//            new InfusionOp<AffiliateCommissionList>.SearchCommissions(Return, since, until, limit, offset, affiliateId);
//
//        public static InfusionOp<ObjectModel> RetrieveAffiliateModel() =>
//            new InfusionOp<ObjectModel>.RetrieveAffiliateModel(Return);
//
//        public static InfusionOp<AppointmentList> ListAppointments(string since, string until, int? limit, int? offset, long? contact_id) =>
//            new InfusionOp<AppointmentList>.ListAppointments(Return, since, until, limit, offset, contact_id);
//
//        public static InfusionOp<Appointment> CreateAppointment(Appointment appointment) =>
//            new InfusionOp<Appointment>.CreateAppointment(Return, appointment);
//
//        public static InfusionOp<ObjectModel> RetrieveAppointmentModel() =>
//            new InfusionOp<ObjectModel>.RetrieveAppointmentModel(Return);
//
//        public static InfusionOp<Appointment> GetAppointment(long appointmentId) =>
//            new InfusionOp<Appointment>.GetAppointment(Return, appointmentId);
//
//        public static InfusionOp<Appointment> UpdateAppointment(long appointmentId, Appointment appointmentDTO) =>
//            new InfusionOp<Appointment>.UpdateAppointment(Return, appointmentId, appointmentDTO);
//
//        public static InfusionOp<Unit> DeleteAppointment(long appointmentId) =>
//            new InfusionOp<Unit>.DeleteAppointment(ReturnUnit, appointmentId);
//
//        public static InfusionOp<Appointment> UpdatePropertiesOnAppointment(long appointmentId, Appointment appointmentDTO) =>
//            new InfusionOp<Appointment>.UpdatePropertiesOnAppointment(Return, appointmentId, appointmentDTO);
//
//        public static InfusionOp<CampaignList> ListCampaigns(int? limit, int? offset, string search_text, Order2? order, Order_direction? order_direction) =>
//            new InfusionOp<CampaignList>.ListCampaigns(Return, limit, offset, search_text, order, order_direction);
//
//        public static InfusionOp<Campaign> GetCampaign(long campaignId, LanguageExt.Lst<string> optional_properties) =>
//            new InfusionOp<Campaign>.GetCampaign(Return, campaignId, optional_properties);
//
//        public static InfusionOp<Unit> AddContactsToCampaignSequence(long campaignId, long sequenceId, SetOfIds ids) =>
//            new InfusionOp<Unit>.AddContactsToCampaignSequence(ReturnUnit, campaignId, sequenceId, ids);
//
//        public static InfusionOp<Unit> RemoveContactsFromCampaignSequence(long campaignId, long sequenceId, SetOfIds ids) =>
//            new InfusionOp<Unit>.RemoveContactsFromCampaignSequence(ReturnUnit, campaignId, sequenceId, ids);
//
//        public static InfusionOp<Unit> AddContactToCampaignSequence(long campaignId, long sequenceId, long contactId) =>
//            new InfusionOp<Unit>.AddContactToCampaignSequence(ReturnUnit, campaignId, sequenceId, contactId);
//
//        public static InfusionOp<Unit> RemoveContactFromCampaignSequence(long campaignId, long sequenceId, long contactId) =>
//            new InfusionOp<Unit>.RemoveContactFromCampaignSequence(ReturnUnit, campaignId, sequenceId, contactId);
//
//        public static InfusionOp<CompanyList> ListCompanies(int? limit, int? offset, string company_name, Order3? order, Order_direction2? order_direction, LanguageExt.Lst<string> optional_properties) =>
//            new InfusionOp<CompanyList>.ListCompanies(Return, limit, offset, company_name, order, order_direction, optional_properties);
//
//        public static InfusionOp<Company> CreateCompany(CreateCompany company) =>
//            new InfusionOp<Company>.CreateCompany(Return, company);
//
//        public static InfusionOp<ObjectModel> RetrieveCompanyModel() =>
//            new InfusionOp<ObjectModel>.RetrieveCompanyModel(Return);
//
//        public static InfusionOp<ContactList> ListContacts(int? limit, int? offset, string email, string given_name, string family_name, Order4? order, Order_direction3? order_direction) =>
//            new InfusionOp<ContactList>.ListContacts(Return, limit, offset, email, given_name, family_name, order, order_direction);
//
//        public static InfusionOp<FullContact> CreateContact(RequestContact contact) =>
//            new InfusionOp<FullContact>.CreateContact(Return, contact);
//
//        public static InfusionOp<FullContact> CreateOrUpdateContact(UpsertContact contact) =>
//            new InfusionOp<FullContact>.CreateOrUpdateContact(Return, contact);
//
//        public static InfusionOp<ObjectModel> RetrieveContactModel() =>
//            new InfusionOp<ObjectModel>.RetrieveContactModel(Return);
//
//        public static InfusionOp<Unit> DeleteContact(long contactId) =>
//            new InfusionOp<Unit>.DeleteContact(ReturnUnit, contactId);
//
//        public static InfusionOp<FullContact> UpdatePropertiesOnContact(RequestContact contact, long contactId) =>
//            new InfusionOp<FullContact>.UpdatePropertiesOnContact(Return, contact, contactId);
//
//        public static InfusionOp<CreditCardAdded> CreateCreditCard(CreditCard creditCard, long contactId) =>
//            new InfusionOp<CreditCardAdded>.CreateCreditCard(Return, creditCard, contactId);
//
//        public static InfusionOp<EmailSentQueryResultList> ListEmailsForContact(long contactId, int? limit, int? offset, long? contact_id, string email) =>
//            new InfusionOp<EmailSentQueryResultList>.ListEmailsForContact(Return, contactId, limit, offset, contact_id, email);
//
//        public static InfusionOp<EmailSentCreate> CreateEmailForContact(long contactId, EmailSentCreate emailWithContent) =>
//            new InfusionOp<EmailSentCreate>.CreateEmailForContact(Return, contactId, emailWithContent);
//
//        public static InfusionOp<ContactTagList> ListAppliedTags(long contactId, int? limit, int? offset) =>
//            new InfusionOp<ContactTagList>.ListAppliedTags(Return, contactId, limit, offset);
//
//        public static InfusionOp<LanguageExt.Lst<Entry_longAndString_>> ApplyTagsToContactId(long contactId, TagId tagIds) =>
//            new InfusionOp<LanguageExt.Lst<Entry_longAndString_>>.ApplyTagsToContactId(Return, contactId, tagIds);
//
//        public static InfusionOp<Unit> RemoveTagsFromContact(long contactId, string ids) =>
//            new InfusionOp<Unit>.RemoveTagsFromContact(ReturnUnit, contactId, ids);
//
//        public static InfusionOp<FullContact> GetContact(long id, LanguageExt.Lst<string> optional_properties) =>
//            new InfusionOp<FullContact>.GetContact(Return, id, optional_properties);
//
//        public static InfusionOp<EmailSentQueryResultList> ListEmails(int? limit, int? offset, long? contact_id, string email) =>
//            new InfusionOp<EmailSentQueryResultList>.ListEmails(Return, limit, offset, contact_id, email);
//
//        public static InfusionOp<EmailSentCreate> CreateEmail(EmailSentCreate emailWithContent) =>
//            new InfusionOp<EmailSentCreate>.CreateEmail(Return, emailWithContent);
//
//        public static InfusionOp<EmailSentCreateList> CreateEmails(EmailSentCreateList emailWithContent) =>
//            new InfusionOp<EmailSentCreateList>.CreateEmails(Return, emailWithContent);
//
//        public static InfusionOp<LanguageExt.Map<string, string>> DeleteEmails(SetOfIds emailIds) =>
//            new InfusionOp<LanguageExt.Map<string, string>>.DeleteEmails(Return, emailIds);
//
//        public static InfusionOp<EmailSentQueryResultWithContent> GetEmail(long id) =>
//            new InfusionOp<EmailSentQueryResultWithContent>.GetEmail(Return, id);
//
//        public static InfusionOp<EmailSentCreate> UpdateEmail(EmailSentCreate emailWithContent, long id) =>
//            new InfusionOp<EmailSentCreate>.UpdateEmail(Return, emailWithContent, id);
//
//        public static InfusionOp<Unit> DeleteEmail(long id) =>
//            new InfusionOp<Unit>.DeleteEmail(ReturnUnit, id);
//
//        public static InfusionOp<FileList> ListFiles(int? limit, int? offset, Viewable? viewable, Permission? permission, Infusion.Client.Type? type, string name) =>
//            new InfusionOp<FileList>.ListFiles(Return, limit, offset, viewable, permission, type, name);
//
//        public static InfusionOp<FileInformation> CreateFile(FileUpload fileUpload) =>
//            new InfusionOp<FileInformation>.CreateFile(Return, fileUpload);
//
//        public static InfusionOp<FileInformation> GetFile(long fileId, LanguageExt.Lst<string> optional_properties) =>
//            new InfusionOp<FileInformation>.GetFile(Return, fileId, optional_properties);
//
//        public static InfusionOp<FileInformation> UpdateFile(long fileId, FileUpload fileUpload) =>
//            new InfusionOp<FileInformation>.UpdateFile(Return, fileId, fileUpload);
//
//        public static InfusionOp<Unit> DeleteFile(long fileId) =>
//            new InfusionOp<Unit>.DeleteFile(ReturnUnit, fileId);
//
//        public static InfusionOp<LanguageExt.Lst<RestHook>> List_stored_hook_subscriptions() =>
//            new InfusionOp<LanguageExt.Lst<RestHook>>.List_stored_hook_subscriptions(Return);
//
//        public static InfusionOp<RestHook> Create_a_hook_subscription(RestHookRequest restHookRequest) =>
//            new InfusionOp<RestHook>.Create_a_hook_subscription(Return, restHookRequest);
//
//        public static InfusionOp<LanguageExt.Lst<string>> List_hook_event_types() =>
//            new InfusionOp<LanguageExt.Lst<string>>.List_hook_event_types(Return);
//
//        public static InfusionOp<RestHook> Retrieve_a_hook_subscription(string key) =>
//            new InfusionOp<RestHook>.Retrieve_a_hook_subscription(Return, key);
//
//        public static InfusionOp<RestHook> Update_a_hook_subscription(string key, RestHookRequest restHookRequest) =>
//            new InfusionOp<RestHook>.Update_a_hook_subscription(Return, key, restHookRequest);
//
//        public static InfusionOp<Unit> Delete_a_hook_subscription(string key) =>
//            new InfusionOp<Unit>.Delete_a_hook_subscription(ReturnUnit, key);
//
//        public static InfusionOp<RestHook> Verify_a_hook_subscription_delayed(string key, string x_Hook_Secret) =>
//            new InfusionOp<RestHook>.Verify_a_hook_subscription_delayed(Return, key, x_Hook_Secret);
//
//        public static InfusionOp<RestHook> Verify_a_hook_subscription(string key) =>
//            new InfusionOp<RestHook>.Verify_a_hook_subscription(Return, key);
//
//        public static InfusionOp<UserInfoDTO> GetUserInfo() =>
//            new InfusionOp<UserInfoDTO>.GetUserInfo(Return);
//
//        public static InfusionOp<OpportunityList> ListOpportunities(int? limit, int? offset, long? user_id, long? stage_id, string search_term, Order5? order) =>
//            new InfusionOp<OpportunityList>.ListOpportunities(Return, limit, offset, user_id, stage_id, search_term, order);
//
//        public static InfusionOp<Opportunity> CreateOpportunity(Opportunity opportunity) =>
//            new InfusionOp<Opportunity>.CreateOpportunity(Return, opportunity);
//
//        public static InfusionOp<Opportunity> UpdateOpportunity(Opportunity opportunity) =>
//            new InfusionOp<Opportunity>.UpdateOpportunity(Return, opportunity);
//
//        public static InfusionOp<ObjectModel> RetrieveOpportunityModel() =>
//            new InfusionOp<ObjectModel>.RetrieveOpportunityModel(Return);
//
//        public static InfusionOp<Opportunity> GetOpportunity(long opportunityId, LanguageExt.Lst<string> optional_properties) =>
//            new InfusionOp<Opportunity>.GetOpportunity(Return, opportunityId, optional_properties);
//
//        public static InfusionOp<Opportunity> UpdatePropertiesOnOpportunity(long opportunityId, Opportunity opportunity) =>
//            new InfusionOp<Opportunity>.UpdatePropertiesOnOpportunity(Return, opportunityId, opportunity);
//
//        public static InfusionOp<LanguageExt.Lst<SalesPipeline>> ListOpportunityStagePipelines() =>
//            new InfusionOp<LanguageExt.Lst<SalesPipeline>>.ListOpportunityStagePipelines(Return);
//
//        public static InfusionOp<OrderList> ListOrders(string since, string until, int? limit, int? offset, bool? paid, string order, long? contact_id, long? product_id) =>
//            new InfusionOp<OrderList>.ListOrders(Return, since, until, limit, offset, paid, order, contact_id, product_id);
//
//        public static InfusionOp<ObjectModel> RetrieveOrderModel() =>
//            new InfusionOp<ObjectModel>.RetrieveOrderModel(Return);
//
//        public static InfusionOp<Order> GetOrder(long orderId) =>
//            new InfusionOp<Order>.GetOrder(Return, orderId);
//
//        public static InfusionOp<TransactionList> ListTransactionsForOrder(long orderId, string since, string until, int? limit, int? offset, long? contact_id) =>
//            new InfusionOp<TransactionList>.ListTransactionsForOrder(Return, orderId, since, until, limit, offset, contact_id);
//
//        public static InfusionOp<ProductList> ListProducts(int? limit, int? offset, bool? active) =>
//            new InfusionOp<ProductList>.ListProducts(Return, limit, offset, active);
//
//        public static InfusionOp<ProductStatusList> ListProductsFromSyncToken(string sync_token, int? limit, int? offset) =>
//            new InfusionOp<ProductStatusList>.ListProductsFromSyncToken(Return, sync_token, limit, offset);
//
//        public static InfusionOp<Product> GetProduct(long productId) =>
//            new InfusionOp<Product>.GetProduct(Return, productId);
//
//        public static InfusionOp<Setting> GetApplicationEnabled() =>
//            new InfusionOp<Setting>.GetApplicationEnabled(Return);
//
//        public static InfusionOp<Setting> GetContactOptionTypes() =>
//            new InfusionOp<Setting>.GetContactOptionTypes(Return);
//
//        public static InfusionOp<ObjectModel> RetrieveSubscriptionModel() =>
//            new InfusionOp<ObjectModel>.RetrieveSubscriptionModel(Return);
//
//        public static InfusionOp<Tags> ListTags(int? limit, int? offset, long? category) =>
//            new InfusionOp<Tags>.ListTags(Return, limit, offset, category);
//
//        public static InfusionOp<Tag> CreateTag(CreateTag tag) =>
//            new InfusionOp<Tag>.CreateTag(Return, tag);
//
//        public static InfusionOp<TagCategory> CreateTagCategory(CreateTagCategory tagCategory) =>
//            new InfusionOp<TagCategory>.CreateTagCategory(Return, tagCategory);
//
//        public static InfusionOp<Tag> GetTag(long id) =>
//            new InfusionOp<Tag>.GetTag(Return, id);
//
//        public static InfusionOp<TaggedContactList> ListContactsForTagId(long tagId, int? limit, int? offset) =>
//            new InfusionOp<TaggedContactList>.ListContactsForTagId(Return, tagId, limit, offset);
//
//        public static InfusionOp<LanguageExt.Lst<Entry_longAndString_>> ApplyTagToContactIds(long tagId, SetOfIds ids) =>
//            new InfusionOp<LanguageExt.Lst<Entry_longAndString_>>.ApplyTagToContactIds(Return, tagId, ids);
//
//        public static InfusionOp<Unit> RemoveTagFromContactIds(long tagId, LanguageExt.Lst<long> ids) =>
//            new InfusionOp<Unit>.RemoveTagFromContactIds(ReturnUnit, tagId, ids);
//
//        public static InfusionOp<Unit> RemoveTagFromContactId(long tagId, long contactId) =>
//            new InfusionOp<Unit>.RemoveTagFromContactId(ReturnUnit, tagId, contactId);
//
//        public static InfusionOp<TaskList> ListTasks(long? contact_id, bool? has_due_date, long? user_id, string since, string until, bool? completed, int? limit, int? offset, string order) =>
//            new InfusionOp<TaskList>.ListTasks(Return, contact_id, has_due_date, user_id, since, until, completed, limit, offset, order);
//
//        public static InfusionOp<Infusion.Client.Task> CreateTask(Infusion.Client.Task task) =>
//            new InfusionOp<Infusion.Client.Task>.CreateTask(Return, task);
//
//        public static InfusionOp<ObjectModel> RetrieveTaskModel() =>
//            new InfusionOp<ObjectModel>.RetrieveTaskModel(Return);
//
//        public static InfusionOp<TaskList> ListTasksForCurrentUser(long? contact_id, bool? has_due_date, long? user_id, string since, string until, bool? completed, int? limit, int? offset, string order) =>
//            new InfusionOp<TaskList>.ListTasksForCurrentUser(Return, contact_id, has_due_date, user_id, since, until, completed, limit, offset, order);
//
//        public static InfusionOp<Infusion.Client.Task> GetTask(string taskId) =>
//            new InfusionOp<Infusion.Client.Task>.GetTask(Return, taskId);
//
//        public static InfusionOp<Infusion.Client.Task> UpdateTask(string taskId, Infusion.Client.Task task) =>
//            new InfusionOp<Infusion.Client.Task>.UpdateTask(Return, taskId, task);
//
//        public static InfusionOp<Unit> DeleteTask(string taskId) =>
//            new InfusionOp<Unit>.DeleteTask(ReturnUnit, taskId);
//
//        public static InfusionOp<Infusion.Client.Task> UpdatePropertiesOnTask(string taskId, Infusion.Client.Task task) =>
//            new InfusionOp<Infusion.Client.Task>.UpdatePropertiesOnTask(Return, taskId, task);
//
//        public static InfusionOp<TransactionList> ListTransactions(string since, string until, int? limit, int? offset, long? contact_id) =>
//            new InfusionOp<TransactionList>.ListTransactions(Return, since, until, limit, offset, contact_id);
//
//        public static InfusionOp<Transaction> GetTransaction(long transactionId) =>
//            new InfusionOp<Transaction>.GetTransaction(Return, transactionId);

    }
}