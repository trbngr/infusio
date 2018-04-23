using System;
using System.Diagnostics.CodeAnalysis;

namespace Infusion
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class InfusionOpExtensions
    {
        public static InfusionOp<B> Map<A, B>(this InfusionOp<A> op, Func<A, B> fn) =>
            op.Bind(a => InfusionDsl.Return(fn(a)));

        public static InfusionOp<B> Select<A, B>(this InfusionOp<A> op, Func<A, B> fn) =>
            op.Bind(a => InfusionDsl.Return(fn(a)));

        public static InfusionOp<C> SelectMany<A, B, C>(this InfusionOp<A> op, Func<A, InfusionOp<B>> bind, Func<A, B, C> project) =>
            op.Bind(a => bind(a).Select(b => project(a, b)));

        static InfusionOp<B> Bind<A, B>(this InfusionOp<A> op, Func<A, InfusionOp<B>> fn) =>
            op is InfusionOp<A>.Return rt                               ? fn(rt.Value) :

//        // @formatter:off
//        static InfusionOp<B> Bind<A, B>(this InfusionOp<A> op, Func<A, InfusionOp<B>> fn) =>
//            op is InfusionOp<A>.Return rt                               ? fn(rt.Value) :
//            op is InfusionOp<A>.GetAccountProfile  _1                   ? new InfusionOp<B>.GetAccountProfile(x => _1.Next(x).Bind(fn)) :
//            op is InfusionOp<A>.UpdateAccountInfo  _2                   ? new InfusionOp<B>.UpdateAccountInfo(x => _2.Next(x).Bind(fn), _2.AccountInfo) :
//            op is InfusionOp<A>.SearchCommissions  _3                   ? new InfusionOp<B>.SearchCommissions(x => _3.Next(x).Bind(fn), _3.Since, _3.Until, _3.Limit, _3.Offset, _3.AffiliateId) :
//            op is InfusionOp<A>.RetrieveAffiliateModel  _4              ? new InfusionOp<B>.RetrieveAffiliateModel(x => _4.Next(x).Bind(fn)) :
//            op is InfusionOp<A>.ListAppointments  _5                    ? new InfusionOp<B>.ListAppointments(x => _5.Next(x).Bind(fn), _5.Since, _5.Until, _5.Limit, _5.Offset, _5.Contact_id) :
//            op is InfusionOp<A>.CreateAppointment  _6                   ? new InfusionOp<B>.CreateAppointment(x => _6.Next(x).Bind(fn), _6.Appointment) :
//            op is InfusionOp<A>.RetrieveAppointmentModel  _7            ? new InfusionOp<B>.RetrieveAppointmentModel(x => _7.Next(x).Bind(fn)) :
//            op is InfusionOp<A>.GetAppointment  _8                      ? new InfusionOp<B>.GetAppointment(x => _8.Next(x).Bind(fn), _8.AppointmentId) :
//            op is InfusionOp<A>.UpdateAppointment  _9                   ? new InfusionOp<B>.UpdateAppointment(x => _9.Next(x).Bind(fn), _9.AppointmentId, _9.AppointmentDTO) :
//            op is InfusionOp<A>.DeleteAppointment  _10                  ? new InfusionOp<B>.DeleteAppointment(() => _10.Next().Bind(fn), _10.AppointmentId) :
//            op is InfusionOp<A>.UpdatePropertiesOnAppointment  _11      ? new InfusionOp<B>.UpdatePropertiesOnAppointment(x => _11.Next(x).Bind(fn), _11.AppointmentId, _11.AppointmentDTO) :
//            op is InfusionOp<A>.ListCampaigns  _12                      ? new InfusionOp<B>.ListCampaigns(x => _12.Next(x).Bind(fn), _12.Limit, _12.Offset, _12.Search_text, _12.Order, _12.Order_direction) :
//            op is InfusionOp<A>.GetCampaign  _13                        ? new InfusionOp<B>.GetCampaign(x => _13.Next(x).Bind(fn), _13.CampaignId, _13.Optional_properties) :
//            op is InfusionOp<A>.AddContactsToCampaignSequence  _14      ? new InfusionOp<B>.AddContactsToCampaignSequence(() => _14.Next().Bind(fn), _14.CampaignId, _14.SequenceId, _14.Ids) :
//            op is InfusionOp<A>.RemoveContactsFromCampaignSequence  _15 ? new InfusionOp<B>.RemoveContactsFromCampaignSequence(() => _15.Next().Bind(fn), _15.CampaignId, _15.SequenceId, _15.Ids) :
//            op is InfusionOp<A>.AddContactToCampaignSequence  _16       ? new InfusionOp<B>.AddContactToCampaignSequence(() => _16.Next().Bind(fn), _16.CampaignId, _16.SequenceId, _16.ContactId) :
//            op is InfusionOp<A>.RemoveContactFromCampaignSequence  _17  ? new InfusionOp<B>.RemoveContactFromCampaignSequence(() => _17.Next().Bind(fn), _17.CampaignId, _17.SequenceId, _17.ContactId) :
//            op is InfusionOp<A>.ListCompanies  _18                      ? new InfusionOp<B>.ListCompanies(x => _18.Next(x).Bind(fn), _18.Limit, _18.Offset, _18.Company_name, _18.Order, _18.Order_direction, _18.Optional_properties) :
//            op is InfusionOp<A>.CreateCompany  _19                      ? new InfusionOp<B>.CreateCompany(x => _19.Next(x).Bind(fn), _19.Company) :
//            op is InfusionOp<A>.RetrieveCompanyModel  _20               ? new InfusionOp<B>.RetrieveCompanyModel(x => _20.Next(x).Bind(fn)) :
//            op is InfusionOp<A>.ListContacts  _21                       ? new InfusionOp<B>.ListContacts(x => _21.Next(x).Bind(fn), _21.Limit, _21.Offset, _21.Email, _21.Given_name, _21.Family_name, _21.Order, _21.Order_direction) :
//            op is InfusionOp<A>.CreateContact  _22                      ? new InfusionOp<B>.CreateContact(x => _22.Next(x).Bind(fn), _22.Contact) :
//            op is InfusionOp<A>.CreateOrUpdateContact  _23              ? new InfusionOp<B>.CreateOrUpdateContact(x => _23.Next(x).Bind(fn), _23.Contact) :
//            op is InfusionOp<A>.RetrieveContactModel  _24               ? new InfusionOp<B>.RetrieveContactModel(x => _24.Next(x).Bind(fn)) :
//            op is InfusionOp<A>.DeleteContact  _25                      ? new InfusionOp<B>.DeleteContact(() => _25.Next().Bind(fn), _25.ContactId) :
//            op is InfusionOp<A>.UpdatePropertiesOnContact  _26          ? new InfusionOp<B>.UpdatePropertiesOnContact(x => _26.Next(x).Bind(fn), _26.Contact, _26.ContactId) :
//            op is InfusionOp<A>.CreateCreditCard  _27                   ? new InfusionOp<B>.CreateCreditCard(x => _27.Next(x).Bind(fn), _27.CreditCard, _27.ContactId) :
//            op is InfusionOp<A>.ListEmailsForContact  _28               ? new InfusionOp<B>.ListEmailsForContact(x => _28.Next(x).Bind(fn), _28.ContactId, _28.Limit, _28.Offset, _28.Contact_id, _28.Email) :
//            op is InfusionOp<A>.CreateEmailForContact  _29              ? new InfusionOp<B>.CreateEmailForContact(x => _29.Next(x).Bind(fn), _29.ContactId, _29.EmailWithContent) :
//            op is InfusionOp<A>.ListAppliedTags  _30                    ? new InfusionOp<B>.ListAppliedTags(x => _30.Next(x).Bind(fn), _30.ContactId, _30.Limit, _30.Offset) :
//            op is InfusionOp<A>.ApplyTagsToContactId  _31               ? new InfusionOp<B>.ApplyTagsToContactId(x => _31.Next(x).Bind(fn), _31.ContactId, _31.TagIds) :
//            op is InfusionOp<A>.RemoveTagsFromContact  _32              ? new InfusionOp<B>.RemoveTagsFromContact(() => _32.Next().Bind(fn), _32.ContactId, _32.Ids) :
//            op is InfusionOp<A>.GetContact  _34                         ? new InfusionOp<B>.GetContact(x => _34.Next(x).Bind(fn), _34.Id, _34.Optional_properties) :
//            op is InfusionOp<A>.ListEmails  _35                         ? new InfusionOp<B>.ListEmails(x => _35.Next(x).Bind(fn), _35.Limit, _35.Offset, _35.Contact_id, _35.Email) :
//            op is InfusionOp<A>.CreateEmail  _36                        ? new InfusionOp<B>.CreateEmail(x => _36.Next(x).Bind(fn), _36.EmailWithContent) :
//            op is InfusionOp<A>.CreateEmails  _37                       ? new InfusionOp<B>.CreateEmails(x => _37.Next(x).Bind(fn), _37.EmailWithContent) :
//            op is InfusionOp<A>.DeleteEmails  _38                       ? new InfusionOp<B>.DeleteEmails(x => _38.Next(x).Bind(fn), _38.EmailIds) :
//            op is InfusionOp<A>.GetEmail  _39                           ? new InfusionOp<B>.GetEmail(x => _39.Next(x).Bind(fn), _39.Id) :
//            op is InfusionOp<A>.UpdateEmail  _40                        ? new InfusionOp<B>.UpdateEmail(x => _40.Next(x).Bind(fn), _40.EmailWithContent, _40.Id) :
//            op is InfusionOp<A>.DeleteEmail  _41                        ? new InfusionOp<B>.DeleteEmail(() => _41.Next().Bind(fn), _41.Id) :
//            op is InfusionOp<A>.ListFiles  _42                          ? new InfusionOp<B>.ListFiles(x => _42.Next(x).Bind(fn), _42.Limit, _42.Offset, _42.Viewable, _42.Permission, _42.Type, _42.Name) :
//            op is InfusionOp<A>.CreateFile  _43                         ? new InfusionOp<B>.CreateFile(x => _43.Next(x).Bind(fn), _43.FileUpload) :
//            op is InfusionOp<A>.GetFile  _44                            ? new InfusionOp<B>.GetFile(x => _44.Next(x).Bind(fn), _44.FileId, _44.Optional_properties) :
//            op is InfusionOp<A>.UpdateFile  _45                         ? new InfusionOp<B>.UpdateFile(x => _45.Next(x).Bind(fn), _45.FileId, _45.FileUpload) :
//            op is InfusionOp<A>.DeleteFile  _46                         ? new InfusionOp<B>.DeleteFile(() => _46.Next().Bind(fn), _46.FileId) :
//            op is InfusionOp<A>.List_stored_hook_subscriptions  _47     ? new InfusionOp<B>.List_stored_hook_subscriptions(x => _47.Next(x).Bind(fn)) :
//            op is InfusionOp<A>.Create_a_hook_subscription  _48         ? new InfusionOp<B>		.Create_a_hook_subscription(x => _48.Next(x).Bind(fn), _48.RestHookRequest) :
//            op is InfusionOp<A>.List_hook_event_types  _49              ? new InfusionOp<B>.List_hook_event_types(x => _49.Next(x).Bind(fn)) :
//            op is InfusionOp<A>.Retrieve_a_hook_subscription  _50       ? new InfusionOp<B>.Retrieve_a_hook_subscription(x => _50.Next(x).Bind(fn), _50.Key) :
//            op is InfusionOp<A>.Update_a_hook_subscription  _51         ? new InfusionOp<B>.Update_a_hook_subscription(x => _51.Next(x).Bind(fn), _51.Key, _51.RestHookRequest) :
//            op is InfusionOp<A>.Delete_a_hook_subscription  _52         ? new InfusionOp<B>.Delete_a_hook_subscription(() => _52.Next().Bind(fn), _52.Key) :
//            op is InfusionOp<A>.Verify_a_hook_subscription_delayed  _53 ? new InfusionOp<B>.Verify_a_hook_subscription_delayed(x => _53.Next(x).Bind(fn), _53.Key, _53.X_Hook_Secret) :
//            op is InfusionOp<A>.Verify_a_hook_subscription  _54         ? new InfusionOp<B>.Verify_a_hook_subscription(x => _54.Next(x).Bind(fn), _54.Key) :
//            op is InfusionOp<A>.GetUserInfo  _55                        ? new InfusionOp<B>.GetUserInfo(x => _55.Next(x).Bind(fn)) :
//            op is InfusionOp<A>.ListOpportunities  _56                  ? new InfusionOp<B>.ListOpportunities(x => _56.Next(x).Bind(fn), _56.Limit, _56.Offset, _56.User_id, _56.Stage_id, _56.Search_term, _56.Order) :
//            op is InfusionOp<A>.CreateOpportunity  _57                  ? new InfusionOp<B>.CreateOpportunity(x => _57.Next(x).Bind(fn), _57.Opportunity) :
//            op is InfusionOp<A>.UpdateOpportunity  _58                  ? new InfusionOp<B>.UpdateOpportunity(x => _58.Next(x).Bind(fn), _58.Opportunity) :
//            op is InfusionOp<A>.RetrieveOpportunityModel  _59           ? new InfusionOp<B>.RetrieveOpportunityModel(x => _59.Next(x).Bind(fn)) :
//            op is InfusionOp<A>.GetOpportunity  _60                     ? new InfusionOp<B>.GetOpportunity(x => _60.Next(x).Bind(fn), _60.OpportunityId, _60.Optional_properties) :
//            op is InfusionOp<A>.UpdatePropertiesOnOpportunity  _61      ? new InfusionOp<B>.UpdatePropertiesOnOpportunity(x => _61.Next(x).Bind(fn), _61.OpportunityId, _61.Opportunity) :
//            op is InfusionOp<A>.ListOpportunityStagePipelines  _62      ? new InfusionOp<B>.ListOpportunityStagePipelines(x => _62.Next(x).Bind(fn)) :
//            op is InfusionOp<A>.ListOrders  _63                         ? new InfusionOp<B>.ListOrders(x => _63.Next(x).Bind(fn), _63.Since, _63.Until, _63.Limit, _63.Offset, _63.Paid, _63.Order, _63.Contact_id, _63.Product_id) :
//            op is InfusionOp<A>.RetrieveOrderModel  _64                 ? new InfusionOp<B>.RetrieveOrderModel(x => _64.Next(x).Bind(fn)) :
//            op is InfusionOp<A>.GetOrder  _65                           ? new InfusionOp<B>.GetOrder(x => _65.Next(x).Bind(fn), _65.OrderId) :
//            op is InfusionOp<A>.ListTransactionsForOrder  _66           ? new InfusionOp<B>.ListTransactionsForOrder(x => _66.Next(x).Bind(fn), _66.OrderId, _66.Since, _66.Until, _66.Limit, _66.Offset, _66.Contact_id) :
//            op is InfusionOp<A>.ListProducts  _67                       ? new InfusionOp<B>.ListProducts(x => _67.Next(x).Bind(fn), _67.Limit, _67.Offset, _67.Active) :
//            op is InfusionOp<A>.ListProductsFromSyncToken  _68          ? new InfusionOp<B>.ListProductsFromSyncToken(x => _68.Next(x).Bind(fn), _68.Sync_token, _68.Limit, _68.Offset) :
//            op is InfusionOp<A>.GetProduct  _69                         ? new InfusionOp<B>.GetProduct(x => _69.Next(x).Bind(fn), _69.ProductId) :
//            op is InfusionOp<A>.GetApplicationEnabled  _70              ? new InfusionOp<B>.GetApplicationEnabled(x => _70.Next(x).Bind(fn)) :
//            op is InfusionOp<A>.GetContactOptionTypes  _71              ? new InfusionOp<B>.GetContactOptionTypes(x => _71.Next(x).Bind(fn)) :
//            op is InfusionOp<A>.RetrieveSubscriptionModel  _72          ? new InfusionOp<B>.RetrieveSubscriptionModel(x => _72.Next(x).Bind(fn)) :
//            op is InfusionOp<A>.ListTags  _73                           ? new InfusionOp<B>.ListTags(x => _73.Next(x).Bind(fn), _73.Limit, _73.Offset, _73.Category) :
//            op is InfusionOp<A>.CreateTag  _74                          ? new InfusionOp<B>.CreateTag(x => _74.Next(x).Bind(fn), _74.Tag) :
//            op is InfusionOp<A>.CreateTagCategory  _75                  ? new InfusionOp<B>.CreateTagCategory(x => _75.Next(x).Bind(fn), _75.TagCategory) :
//            op is InfusionOp<A>.GetTag  _76                             ? new InfusionOp<B>.GetTag(x => _76.Next(x).Bind(fn), _76.Id) :
//            op is InfusionOp<A>.ListContactsForTagId  _77               ? new InfusionOp<B>.ListContactsForTagId(x => _77.Next(x).Bind(fn), _77.TagId, _77.Limit, _77.Offset) :
//            op is InfusionOp<A>.ApplyTagToContactIds  _78               ? new InfusionOp<B>.ApplyTagToContactIds(x => _78.Next(x).Bind(fn), _78.TagId, _78.Ids) :
//            op is InfusionOp<A>.RemoveTagFromContactIds  _79            ? new InfusionOp<B>.RemoveTagFromContactIds(() => _79.Next().Bind(fn), _79.TagId, _79.Ids) :
//            op is InfusionOp<A>.RemoveTagFromContactId  _80             ? new InfusionOp<B>.RemoveTagFromContactId(() => _80.Next().Bind(fn), _80.TagId, _80.ContactId) :
//            op is InfusionOp<A>.ListTasks  _81                          ? new InfusionOp<B>.ListTasks(x => _81.Next(x).Bind(fn), _81.Contact_id, _81.Has_due_date, _81.User_id, _81.Since, _81.Until, _81.Completed, _81.Limit, _81.Offset, _81.Order) :
//            op is InfusionOp<A>.CreateTask  _82                         ? new InfusionOp<B>.CreateTask(x => _82.Next(x).Bind(fn), _82.Task) :
//            op is InfusionOp<A>.RetrieveTaskModel  _83                  ? new InfusionOp<B>.RetrieveTaskModel(x => _83.Next(x).Bind(fn)) :
//            op is InfusionOp<A>.ListTasksForCurrentUser  _84            ? new InfusionOp<B>.ListTasksForCurrentUser(x => _84.Next(x).Bind(fn), _84.Contact_id, _84.Has_due_date, _84.User_id, _84.Since, _84.Until, _84.Completed, _84.Limit, _84.Offset, _84.Order) :
//            op is InfusionOp<A>.GetTask  _85                            ? new InfusionOp<B>.GetTask(x => _85.Next(x).Bind(fn), _85.TaskId) :
//            op is InfusionOp<A>.UpdateTask  _86                         ? new InfusionOp<B>.UpdateTask(x => _86.Next(x).Bind(fn), _86.TaskId, _86.Task) :
//            op is InfusionOp<A>.DeleteTask  _87                         ? new InfusionOp<B>.DeleteTask(() => _87.Next().Bind(fn), _87.TaskId) :
//            op is InfusionOp<A>.UpdatePropertiesOnTask  _88             ? new InfusionOp<B>.UpdatePropertiesOnTask(x => _88.Next(x).Bind(fn), _88.TaskId, _88.Task) :
//            op is InfusionOp<A>.ListTransactions  _89                   ? new InfusionOp<B>.ListTransactions(x => _89.Next(x).Bind(fn), _89.Since, _89.Until, _89.Limit, _89.Offset, _89.Contact_id) :
//            op is InfusionOp<A>.GetTransaction  _90                     ? new InfusionOp<B>.GetTransaction(x => _90.Next(x).Bind(fn), _90.TransactionId) as InfusionOp<B>  :
//                                                                          throw new NotSupportedException();
//        // @formatter:on

//        public static InfusionOp<B> Map<A, B>(this InfusionOp<A> op, Func<A, B> fn) =>
//            op.Bind(a => InfusionDsl.Return(fn(a)));
//
//        public static InfusionOp<B> Select<A, B>(this InfusionOp<A> op, Func<A, B> fn) =>
//            op.Bind(a => InfusionDsl.Return(fn(a)));
//
//        public static InfusionOp<C> SelectMany<A, B, C>(this InfusionOp<A> op, Func<A, InfusionOp<B>> bind, Func<A, B, C> project) =>
//            op.Bind(a => bind(a).Select(b => project(a, b)));
    }
}