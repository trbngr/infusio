using System;
using System.Net.Http;
using System.Threading.Tasks;
using Infusion.Client;
using Infusionsoft;
using LanguageExt;

namespace Infusion
{
    public static class Interpreter
    {
        static readonly HttpClient Client = new HttpClient();

//        public static Task<Either<Infusion.Model.Error, T>> Interpret<T>(InfusionOp<T> op, InfusionsoftConfig config) =>
//            RunAsync(op, config).ToEither();
//
//        public static Task<Either<Model.Error, T>> Run<T>(this InfusionOp<T> op, InfusionsoftConfig config) =>
//            RunAsync(op, config).ToEither();

//        static EitherAsync<Model.Error, T> RunAsync<T>(InfusionOp<T> op, InfusionsoftConfig config)
//        {
//            var client = new InfusionsoftClient(Client, config);
//
//            // @formatter:off
//            return
//                op is InfusionOp<T>.Return r                                ? Prelude.RightAsync<Model.Error, T>(r.Value.AsTask()) :
//                op is InfusionOp<T>.GetAccountProfile  _1                   ? Execute(() => client.GetAccountProfileUsingGETAsync(),  _1.Next, client) :
//                op is InfusionOp<T>.UpdateAccountInfo  _2                   ? Execute(() => client.UpdateAccountInfoUsingPUTAsync(_2.AccountInfo),  _2.Next, client) :
//                op is InfusionOp<T>.SearchCommissions  _3                   ? Execute(() => client.SearchCommissionsUsingGETAsync(_3.Since, _3.Until, _3.Limit, _3.Offset, _3.AffiliateId),  _3.Next, client) :
//                op is InfusionOp<T>.RetrieveAffiliateModel  _4              ? Execute(() => client.RetrieveAffiliateModelUsingGETAsync(),  _4.Next, client) :
//                op is InfusionOp<T>.ListAppointments  _5                    ? Execute(() => client.ListAppointmentsUsingGETAsync(_5.Since, _5.Until, _5.Limit, _5.Offset, _5.Contact_id),  _5.Next, client) :
//                op is InfusionOp<T>.CreateAppointment  _6                   ? Execute(() => client.CreateAppointmentUsingPOSTAsync(_6.Appointment),  _6.Next, client) :
//                op is InfusionOp<T>.RetrieveAppointmentModel  _7            ? Execute(() => client.RetrieveAppointmentModelUsingGETAsync(),  _7.Next, client) :
//                op is InfusionOp<T>.GetAppointment  _8                      ? Execute(() => client.GetAppointmentUsingGETAsync(_8.AppointmentId),  _8.Next, client) :
//                op is InfusionOp<T>.UpdateAppointment  _9                   ? Execute(() => client.UpdateAppointmentUsingPUTAsync(_9.AppointmentId, _9.AppointmentDTO),  _9.Next, client) :
//                op is InfusionOp<T>.DeleteAppointment  _10                  ? Execute(() => client.DeleteAppointmentUsingDELETEAsync(_10.AppointmentId).Lift(),  _10.Next, client) :
//                op is InfusionOp<T>.UpdatePropertiesOnAppointment  _11      ? Execute(() => client.UpdatePropertiesOnAppointmentUsingPATCHAsync(_11.AppointmentId, _11.AppointmentDTO),  _11.Next, client) :
//                op is InfusionOp<T>.ListCampaigns  _12                      ? Execute(() => client.ListCampaignsUsingGETAsync(_12.Limit, _12.Offset, _12.Search_text, _12.Order, _12.Order_direction),  _12.Next, client) :
//                op is InfusionOp<T>.GetCampaign  _13                        ? Execute(() => client.GetCampaignUsingGETAsync(_13.CampaignId, _13.Optional_properties),  _13.Next, client) :
//                op is InfusionOp<T>.AddContactsToCampaignSequence  _14      ? Execute(() => client.AddContactsToCampaignSequenceUsingPOSTAsync(_14.CampaignId, _14.SequenceId, _14.Ids).Lift(),  _14.Next, client) :
//                op is InfusionOp<T>.RemoveContactsFromCampaignSequence  _15 ? Execute(() => client.RemoveContactsFromCampaignSequenceUsingDELETEAsync(_15.CampaignId, _15.SequenceId, _15.Ids).Lift(),  _15.Next, client) :
//                op is InfusionOp<T>.AddContactToCampaignSequence  _16       ? Execute(() => client.AddContactToCampaignSequenceUsingPOSTAsync(_16.CampaignId, _16.SequenceId, _16.ContactId).Lift(),  _16.Next, client) :
//                op is InfusionOp<T>.RemoveContactFromCampaignSequence  _17  ? Execute(() => client.RemoveContactFromCampaignSequenceUsingDELETEAsync(_17.CampaignId, _17.SequenceId, _17.ContactId).Lift(),  _17.Next, client) :
//                op is InfusionOp<T>.ListCompanies  _18                      ? Execute(() => client.ListCompaniesUsingGETAsync(_18.Limit, _18.Offset, _18.Company_name, _18.Order, _18.Order_direction, _18.Optional_properties),  _18.Next, client) :
//                op is InfusionOp<T>.CreateCompany  _19                      ? Execute(() => client.CreateCompanyUsingPOSTAsync(_19.Company),  _19.Next, client) :
//                op is InfusionOp<T>.RetrieveCompanyModel  _20               ? Execute(() => client.RetrieveCompanyModelUsingGETAsync(),  _20.Next, client) :
//                op is InfusionOp<T>.ListContacts  _21                       ? Execute(() => client.ListContactsUsingGETAsync(_21.Limit, _21.Offset, _21.Email, _21.Given_name, _21.Family_name, _21.Order, _21.Order_direction),  _21.Next, client) :
//                op is InfusionOp<T>.CreateContact  _22                      ? Execute(() => client.CreateContactUsingPOSTAsync(_22.Contact),  _22.Next, client) :
//                op is InfusionOp<T>.CreateOrUpdateContact  _23              ? Execute(() => client.CreateOrUpdateContactUsingPUTAsync(_23.Contact),  _23.Next, client) :
//                op is InfusionOp<T>.RetrieveContactModel  _24               ? Execute(() => client.RetrieveContactModelUsingGETAsync(),  _24.Next, client) :
//                op is InfusionOp<T>.DeleteContact  _25                      ? Execute(() => client.DeleteContactUsingDELETEAsync(_25.ContactId).Lift(),  _25.Next, client) :
//                op is InfusionOp<T>.UpdatePropertiesOnContact  _26          ? Execute(() => client.UpdatePropertiesOnContactUsingPATCHAsync(_26.Contact, _26.ContactId),  _26.Next, client) :
//                op is InfusionOp<T>.CreateCreditCard  _27                   ? Execute(() => client.CreateCreditCardUsingPOSTAsync(_27.CreditCard, _27.ContactId),  _27.Next, client) :
//                op is InfusionOp<T>.ListEmailsForContact  _28               ? Execute(() => client.ListEmailsForContactUsingGETAsync(_28.ContactId, _28.Limit, _28.Offset, _28.Contact_id, _28.Email),  _28.Next, client) :
//                op is InfusionOp<T>.CreateEmailForContact  _29              ? Execute(() => client.CreateEmailForContactUsingPOSTAsync(_29.ContactId, _29.EmailWithContent),  _29.Next, client) :
//                op is InfusionOp<T>.ListAppliedTags  _30                    ? Execute(() => client.ListAppliedTagsUsingGETAsync(_30.ContactId, _30.Limit, _30.Offset),  _30.Next, client) :
//                op is InfusionOp<T>.ApplyTagsToContactId  _31               ? Execute(() => client.ApplyTagsToContactIdUsingPOSTAsync(_31.ContactId, _31.TagIds),  _31.Next, client) :
//                op is InfusionOp<T>.RemoveTagsFromContact  _32              ? Execute(() => client.RemoveTagsFromContactUsingDELETE_1Async(_32.ContactId, _32.Ids).Lift(),  _32.Next, client) :
//                op is InfusionOp<T>.GetContact  _34                         ? Execute(() => client.GetContactUsingGETAsync(_34.Id, _34.Optional_properties),  _34.Next, client) :
//                op is InfusionOp<T>.ListEmails  _35                         ? Execute(() => client.ListEmailsUsingGETAsync(_35.Limit, _35.Offset, _35.Contact_id, _35.Email),  _35.Next, client) :
//                op is InfusionOp<T>.CreateEmail  _36                        ? Execute(() => client.CreateEmailUsingPOSTAsync(_36.EmailWithContent),  _36.Next, client) :
//                op is InfusionOp<T>.CreateEmails  _37                       ? Execute(() => client.CreateEmailsUsingPOSTAsync(_37.EmailWithContent),  _37.Next, client) :
//                op is InfusionOp<T>.DeleteEmails  _38                       ? Execute(() => client.DeleteEmailsUsingPOSTAsync(_38.EmailIds),  _38.Next, client) :
//                op is InfusionOp<T>.GetEmail  _39                           ? Execute(() => client.GetEmailUsingGETAsync(_39.Id),  _39.Next, client) :
//                op is InfusionOp<T>.UpdateEmail  _40                        ? Execute(() => client.UpdateEmailUsingPUTAsync(_40.EmailWithContent, _40.Id),  _40.Next, client) :
//                op is InfusionOp<T>.DeleteEmail  _41                        ? Execute(() => client.DeleteEmailUsingDELETEAsync(_41.Id).Lift(),  _41.Next, client) :
//                op is InfusionOp<T>.ListFiles  _42                          ? Execute(() => client.ListFilesUsingGETAsync(_42.Limit, _42.Offset, _42.Viewable, _42.Permission, _42.Type, _42.Name),  _42.Next, client) :
//                op is InfusionOp<T>.CreateFile  _43                         ? Execute(() => client.CreateFileUsingPOSTAsync(_43.FileUpload),  _43.Next, client) :
//                op is InfusionOp<T>.GetFile  _44                            ? Execute(() => client.GetFileUsingGETAsync(_44.FileId, _44.Optional_properties),  _44.Next, client) :
//                op is InfusionOp<T>.UpdateFile  _45                         ? Execute(() => client.UpdateFileUsingPUTAsync(_45.FileId, _45.FileUpload),  _45.Next, client) :
//                op is InfusionOp<T>.DeleteFile  _46                         ? Execute(() => client.DeleteFileUsingDELETEAsync(_46.FileId).Lift(),  _46.Next, client) :
//                op is InfusionOp<T>.List_stored_hook_subscriptions  _47     ? Execute(() => client.List_stored_hook_subscriptionsAsync(),  _47.Next, client) :
//                op is InfusionOp<T>.Create_a_hook_subscription  _48         ? Execute(() => client.Create_a_hook_subscriptionAsync(_48.RestHookRequest),  _48.Next, client) :
//                op is InfusionOp<T>.List_hook_event_types  _49              ? Execute(() => client.List_hook_event_typesAsync(),  _49.Next, client) :
//                op is InfusionOp<T>.Retrieve_a_hook_subscription  _50       ? Execute(() => client.Retrieve_a_hook_subscriptionAsync(_50.Key),  _50.Next, client) :
//                op is InfusionOp<T>.Update_a_hook_subscription  _51         ? Execute(() => client.Update_a_hook_subscriptionAsync(_51.Key, _51.RestHookRequest),  _51.Next, client) :
//                op is InfusionOp<T>.Delete_a_hook_subscription  _52         ? Execute(() => client.Delete_a_hook_subscriptionAsync(_52.Key).Lift(),  _52.Next, client) :
//                op is InfusionOp<T>.Verify_a_hook_subscription_delayed  _53 ? Execute(() => client.Verify_a_hook_subscription_delayedAsync(_53.Key, _53.X_Hook_Secret),  _53.Next, client) :
//                op is InfusionOp<T>.Verify_a_hook_subscription  _54         ? Execute(() => client.Verify_a_hook_subscriptionAsync(_54.Key),  _54.Next, client) :
//                op is InfusionOp<T>.GetUserInfo  _55                        ? Execute(() => client.GetUserInfoUsingGETAsync(),  _55.Next, client) :
//                op is InfusionOp<T>.ListOpportunities  _56                  ? Execute(() => client.ListOpportunitiesUsingGETAsync(_56.Limit, _56.Offset, _56.User_id, _56.Stage_id, _56.Search_term, _56.Order),  _56.Next, client) :
//                op is InfusionOp<T>.CreateOpportunity  _57                  ? Execute(() => client.CreateOpportunityUsingPOSTAsync(_57.Opportunity),  _57.Next, client) :
//                op is InfusionOp<T>.UpdateOpportunity  _58                  ? Execute(() => client.UpdateOpportunityUsingPUTAsync(_58.Opportunity),  _58.Next, client) :
//                op is InfusionOp<T>.RetrieveOpportunityModel  _59           ? Execute(() => client.RetrieveOpportunityModelUsingGETAsync(),  _59.Next, client) :
//                op is InfusionOp<T>.GetOpportunity  _60                     ? Execute(() => client.GetOpportunityUsingGETAsync(_60.OpportunityId, _60.Optional_properties),  _60.Next, client) :
//                op is InfusionOp<T>.UpdatePropertiesOnOpportunity  _61      ? Execute(() => client.UpdatePropertiesOnOpportunityUsingPATCHAsync(_61.OpportunityId, _61.Opportunity),  _61.Next, client) :
//                op is InfusionOp<T>.ListOpportunityStagePipelines  _62      ? Execute(() => client.ListOpportunityStagePipelinesUsingGETAsync(),  _62.Next, client) :
//                op is InfusionOp<T>.ListOrders  _63                         ? Execute(() => client.ListOrdersUsingGETAsync(_63.Since, _63.Until, _63.Limit, _63.Offset, _63.Paid, _63.Order, _63.Contact_id, _63.Product_id),  _63.Next, client) :
//                op is InfusionOp<T>.RetrieveOrderModel  _64                 ? Execute(() => client.RetrieveOrderModelUsingGETAsync(),  _64.Next, client) :
//                op is InfusionOp<T>.GetOrder  _65                           ? Execute(() => client.GetOrderUsingGETAsync(_65.OrderId),  _65.Next, client) :
//                op is InfusionOp<T>.ListTransactionsForOrder  _66           ? Execute(() => client.ListTransactionsForOrderUsingGETAsync(_66.OrderId, _66.Since, _66.Until, _66.Limit, _66.Offset, _66.Contact_id),  _66.Next, client) :
//                op is InfusionOp<T>.ListProducts  _67                       ? Execute(() => client.ListProductsUsingGETAsync(_67.Limit, _67.Offset, _67.Active),  _67.Next, client) :
//                op is InfusionOp<T>.GetProduct  _69                         ? Execute(() => client.GetProductUsingGETAsync(_69.ProductId),  _69.Next, client) :
//                op is InfusionOp<T>.GetApplicationEnabled  _70              ? Execute(() => client.GetApplicationEnabledUsingGETAsync(),  _70.Next, client) :
//                op is InfusionOp<T>.GetContactOptionTypes  _71              ? Execute(() => client.GetContactOptionTypesUsingGETAsync(),  _71.Next, client) :
//                op is InfusionOp<T>.RetrieveSubscriptionModel  _72          ? Execute(() => client.RetrieveSubscriptionModelUsingGETAsync(),  _72.Next, client) :
//                op is InfusionOp<T>.ListTags  _73                           ? Execute(() => client.ListTagsUsingGETAsync(_73.Limit, _73.Offset, _73.Category),  _73.Next, client) :
//                op is InfusionOp<T>.CreateTag  _74                          ? Execute(() => client.CreateTagUsingPOSTAsync(_74.Tag),  _74.Next, client) :
//                op is InfusionOp<T>.CreateTagCategory  _75                  ? Execute(() => client.CreateTagCategoryUsingPOSTAsync(_75.TagCategory),  _75.Next, client) :
//                op is InfusionOp<T>.GetTag  _76                             ? Execute(() => client.GetTagUsingGETAsync(_76.Id),  _76.Next, client) :
//                op is InfusionOp<T>.ListContactsForTagId  _77               ? Execute(() => client.ListContactsForTagIdUsingGETAsync(_77.TagId, _77.Limit, _77.Offset),  _77.Next, client) :
//                op is InfusionOp<T>.ApplyTagToContactIds  _78               ? Execute(() => client.ApplyTagToContactIdsUsingPOSTAsync(_78.TagId, _78.Ids),  _78.Next, client) :
//                op is InfusionOp<T>.RemoveTagFromContactIds  _79            ? Execute(() => client.RemoveTagFromContactIdsUsingDELETEAsync(_79.TagId, _79.Ids).Lift(),  _79.Next, client) :
//                op is InfusionOp<T>.RemoveTagFromContactId  _80             ? Execute(() => client.RemoveTagFromContactIdUsingDELETEAsync(_80.TagId, _80.ContactId).Lift(),  _80.Next, client) :
//                op is InfusionOp<T>.ListTasks  _81                          ? Execute(() => client.ListTasksUsingGETAsync(_81.Contact_id, _81.Has_due_date, _81.User_id, _81.Since, _81.Until, _81.Completed, _81.Limit, _81.Offset, _81.Order),  _81.Next, client) :
//                op is InfusionOp<T>.CreateTask  _82                         ? Execute(() => client.CreateTaskUsingPOSTAsync(_82.Task),  _82.Next, client) :
//                op is InfusionOp<T>.RetrieveTaskModel  _83                  ? Execute(() => client.RetrieveTaskModelUsingGETAsync(),  _83.Next, client) :
//                op is InfusionOp<T>.ListTasksForCurrentUser  _84            ? Execute(() => client.ListTasksForCurrentUserUsingGETAsync(_84.Contact_id, _84.Has_due_date, _84.User_id, _84.Since, _84.Until, _84.Completed, _84.Limit, _84.Offset, _84.Order),  _84.Next, client) :
//                op is InfusionOp<T>.GetTask  _85                            ? Execute(() => client.GetTaskUsingGETAsync(_85.TaskId),  _85.Next, client) :
//                op is InfusionOp<T>.UpdateTask  _86                         ? Execute(() => client.UpdateTaskUsingPUTAsync(_86.TaskId, _86.Task),  _86.Next, client) :
//                op is InfusionOp<T>.DeleteTask  _87                         ? Execute(() => client.DeleteTaskUsingDELETEAsync(_87.TaskId).Lift(),  _87.Next, client) :
//                op is InfusionOp<T>.UpdatePropertiesOnTask  _88             ? Execute(() => client.UpdatePropertiesOnTaskUsingPATCHAsync(_88.TaskId, _88.Task),  _88.Next, client) :
//                op is InfusionOp<T>.ListTransactions  _89                   ? Execute(() => client.ListTransactionsUsingGETAsync(_89.Since, _89.Until, _89.Limit, _89.Offset, _89.Contact_id),  _89.Next, client) :
//                op is InfusionOp<T>.GetTransaction  _90                     ? Execute(() => client.GetTransactionUsingGETAsync(_90.TransactionId),  _90.Next, client) :
//                                                                              throw new NotSupportedException();
//            // @formatter:on
//        }

//        static EitherAsync<Model.Error, B> Execute<T, B>(Func<Task<T>> fn, Func<T, InfusionOp<B>> nextOp, InfusionsoftClient client) =>
//            from profile in AsEither(fn)
//            from next in RunAsync(nextOp(profile), client.Config)
//            select next;
//
//        static EitherAsync<Model.Error, B> Execute<B>(Func<Task<Unit>> fn, Func<InfusionOp<B>> nextOp, InfusionsoftClient client) =>
//            from profile in AsEither(fn)
//            from next in RunAsync(nextOp(), client.Config)
//            select next;
//
//        static EitherAsync<Model.Error, T> AsEither<T>(Func<Task<T>> fn) =>
//            Prelude.TryAsync(() => fn()).Match(
//                Succ: t => Prelude.Right<Model.Error, T>(t),
//                Fail: e => Prelude.Left<Model.Error, T>(new Model.Error())
//            ).ToAsync();
    }
}