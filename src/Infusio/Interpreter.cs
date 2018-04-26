using System;
using System.Threading.Tasks;
using Infusio.Model;
using Infusio.Ops;
using LanguageExt;

namespace Infusio.Http
{
    using static Prelude;

    public static class HttpSupport
    {
        public static Task<Either<Error, T>> interpret<T>(InfusioOp<T> op, InfusioClient client) =>
            RunAsync(op, client);

        public static Task<Either<Error, T>> RunWith<T>(this InfusioOp<T> op, InfusioClient client) =>
            RunAsync(op, client);

        static Task<Either<Error, T>> RunAsync<T>(InfusioOp<T> op, InfusioClient client) =>
            op is InfusioOp<T>.Return r ? Right<Error, T>(r.Value).AsTask() :
            op is InfusioOp<T>.GetAccountProfile _1 ? Exe(() => client.GetAccountProfile(), _1.Next, client) :
            op is InfusioOp<T>.UpdateAccountInfo _2 ? Exe(() => client.UpdateAccountInfo(_2.AccountInfo), _2.Next, client) :
            op is InfusioOp<T>.SearchCommissions _3 ? Exe(() => client.SearchCommissions(_3.AffiliateId, _3.Offset, _3.Limit, _3.Until, _3.Since), _3.Next, client) :
            op is InfusioOp<T>.RetrieveAffiliateModel _4 ? Exe(() => client.RetrieveAffiliateModel(), _4.Next, client) :
            op is InfusioOp<T>.ListAppointments _5 ? Exe(() => client.ListAppointments(_5.ContactId, _5.Offset, _5.Limit, _5.Until, _5.Since), _5.Next, client) :
            op is InfusioOp<T>.CreateAppointment _6 ? Exe(() => client.CreateAppointment(_6.Appointment), _6.Next, client) :
            op is InfusioOp<T>.RetrieveAppointmentModel _7 ? Exe(() => client.RetrieveAppointmentModel(), _7.Next, client) :
            op is InfusioOp<T>.GetAppointment _8 ? Exe(() => client.GetAppointment(_8.AppointmentId), _8.Next, client) :
            op is InfusioOp<T>.UpdateAppointment _9 ? Exe(() => client.UpdateAppointment(_9.AppointmentDTO, _9.AppointmentId), _9.Next, client) :
            op is InfusioOp<T>.DeleteAppointment _10 ? Exe(() => client.DeleteAppointment(_10.AppointmentId), _10.Next, client) :
            op is InfusioOp<T>.UpdatePropertiesOnAppointment _11 ? Exe(() => client.UpdatePropertiesOnAppointment(_11.AppointmentDTO, _11.AppointmentId), _11.Next, client) :
            op is InfusioOp<T>.ListCampaigns _12 ? Exe(() => client.ListCampaigns(_12.OrderDirection, _12.Order, _12.SearchText, _12.Offset, _12.Limit), _12.Next, client) :
            op is InfusioOp<T>.GetCampaign _13 ? Exe(() => client.GetCampaign(_13.CampaignId, _13.OptionalProperties), _13.Next, client) :
            op is InfusioOp<T>.AddContactsToCampaignSequence _14 ? Exe(() => client.AddContactsToCampaignSequence(_14.Ids, _14.SequenceId, _14.CampaignId), _14.Next, client) :
            op is InfusioOp<T>.RemoveContactsFromCampaignSequence _15 ? Exe(() => client.RemoveContactsFromCampaignSequence(_15.Ids, _15.SequenceId, _15.CampaignId), _15.Next, client) :
            op is InfusioOp<T>.AddContactToCampaignSequence _16 ? Exe(() => client.AddContactToCampaignSequence(_16.ContactId, _16.SequenceId, _16.CampaignId), _16.Next, client) :
            op is InfusioOp<T>.RemoveContactFromCampaignSequence _17 ? Exe(() => client.RemoveContactFromCampaignSequence(_17.ContactId, _17.SequenceId, _17.CampaignId), _17.Next, client) :
            op is InfusioOp<T>.ListCompanies _18 ? Exe(() => client.ListCompanies(_18.OptionalProperties, _18.OrderDirection, _18.Order, _18.CompanyName, _18.Offset, _18.Limit), _18.Next, client) :
            op is InfusioOp<T>.CreateCompany _19 ? Exe(() => client.CreateCompany(_19.Company), _19.Next, client) :
            op is InfusioOp<T>.RetrieveCompanyModel _20 ? Exe(() => client.RetrieveCompanyModel(), _20.Next, client) :
            op is InfusioOp<T>.ListContacts _21 ? Exe(() => client.ListContacts(_21.OrderDirection, _21.Order, _21.FamilyName, _21.GivenName, _21.Email, _21.Offset, _21.Limit), _21.Next, client) :
            op is InfusioOp<T>.CreateContact _22 ? Exe(() => client.CreateContact(_22.Contact), _22.Next, client) :
            op is InfusioOp<T>.CreateOrUpdateContact _23 ? Exe(() => client.CreateOrUpdateContact(_23.Contact), _23.Next, client) :
            op is InfusioOp<T>.RetrieveContactModel _24 ? Exe(() => client.RetrieveContactModel(), _24.Next, client) :
            op is InfusioOp<T>.DeleteContact _25 ? Exe(() => client.DeleteContact(_25.ContactId), _25.Next, client) :
            op is InfusioOp<T>.UpdatePropertiesOnContact _26 ? Exe(() => client.UpdatePropertiesOnContact(_26.ContactId, _26.Contact), _26.Next, client) :
            op is InfusioOp<T>.CreateCreditCard _27 ? Exe(() => client.CreateCreditCard(_27.ContactId, _27.CreditCard), _27.Next, client) :
            op is InfusioOp<T>.ListEmailsForContact _28 ? Exe(() => client.ListEmailsForContact(_28.ContactId, _28.Email, _28.ContactId2, _28.Offset, _28.Limit), _28.Next, client) :
            op is InfusioOp<T>.CreateEmailForContact _29 ? Exe(() => client.CreateEmailForContact(_29.ContactId, _29.EmailWithContent), _29.Next, client) :
            op is InfusioOp<T>.ListAppliedTags _30 ? Exe(() => client.ListAppliedTags(_30.ContactId, _30.Offset, _30.Limit), _30.Next, client) :
            op is InfusioOp<T>.ApplyTagsToContactId _31 ? Exe(() => client.ApplyTagsToContactId(_31.TagIds, _31.ContactId), _31.Next, client) :
            op is InfusioOp<T>.RemoveTagsFromContact _32 ? Exe(() => client.RemoveTagsFromContact(_32.Ids, _32.ContactId), _32.Next, client) :
            op is InfusioOp<T>.RemoveTagsFromContact2 _33 ? Exe(() => client.RemoveTagsFromContact2(_33.TagId, _33.ContactId), _33.Next, client) :
            op is InfusioOp<T>.GetContact _34 ? Exe(() => client.GetContact(_34.Id, _34.OptionalProperties), _34.Next, client) :
            op is InfusioOp<T>.ListEmails _35 ? Exe(() => client.ListEmails(_35.Email, _35.ContactId, _35.Offset, _35.Limit), _35.Next, client) :
            op is InfusioOp<T>.CreateEmail _36 ? Exe(() => client.CreateEmail(_36.EmailWithContent), _36.Next, client) :
            op is InfusioOp<T>.CreateEmails _37 ? Exe(() => client.CreateEmails(_37.EmailWithContent), _37.Next, client) :
            op is InfusioOp<T>.DeleteEmails _38 ? Exe(() => client.DeleteEmails(_38.EmailIds), _38.Next, client) :
            op is InfusioOp<T>.GetEmail _39 ? Exe(() => client.GetEmail(_39.Id), _39.Next, client) :
            op is InfusioOp<T>.UpdateEmail _40 ? Exe(() => client.UpdateEmail(_40.Id, _40.EmailWithContent), _40.Next, client) :
            op is InfusioOp<T>.DeleteEmail _41 ? Exe(() => client.DeleteEmail(_41.Id), _41.Next, client) :
            op is InfusioOp<T>.ListFiles _42 ? Exe(() => client.ListFiles(_42.Name, _42.Type, _42.Permission, _42.Viewable, _42.Offset, _42.Limit), _42.Next, client) :
            op is InfusioOp<T>.CreateFile _43 ? Exe(() => client.CreateFile(_43.FileUpload), _43.Next, client) :
            op is InfusioOp<T>.GetFile _44 ? Exe(() => client.GetFile(_44.FileId, _44.OptionalProperties), _44.Next, client) :
            op is InfusioOp<T>.UpdateFile _45 ? Exe(() => client.UpdateFile(_45.FileId, _45.FileUpload), _45.Next, client) :
            op is InfusioOp<T>.DeleteFile _46 ? Exe(() => client.DeleteFile(_46.FileId), _46.Next, client) :
            op is InfusioOp<T>.ListStoredHookSubscriptions _47 ? Exe(() => client.ListStoredHookSubscriptions(), _47.Next, client) :
            op is InfusioOp<T>.CreateAHookSubscription _48 ? Exe(() => client.CreateAHookSubscription(_48.RestHookRequest), _48.Next, client) :
            op is InfusioOp<T>.ListHookEventTypes _49 ? Exe(() => client.ListHookEventTypes(), _49.Next, client) :
            op is InfusioOp<T>.RetrieveAHookSubscription _50 ? Exe(() => client.RetrieveAHookSubscription(_50.Key), _50.Next, client) :
            op is InfusioOp<T>.UpdateAHookSubscription _51 ? Exe(() => client.UpdateAHookSubscription(_51.RestHookRequest, _51.Key), _51.Next, client) :
            op is InfusioOp<T>.DeleteAHookSubscription _52 ? Exe(() => client.DeleteAHookSubscription(_52.Key), _52.Next, client) :
            op is InfusioOp<T>.VerifyAHookSubscriptionDelayed _53 ? Exe(() => client.VerifyAHookSubscriptionDelayed(_53.XHookSecret, _53.Key), _53.Next, client) :
            op is InfusioOp<T>.VerifyAHookSubscription _54 ? Exe(() => client.VerifyAHookSubscription(_54.Key), _54.Next, client) :
            op is InfusioOp<T>.GetUserInfo _55 ? Exe(() => client.GetUserInfo(), _55.Next, client) :
            op is InfusioOp<T>.ListOpportunities _56 ? Exe(() => client.ListOpportunities(_56.Order, _56.SearchTerm, _56.StageId, _56.UserId, _56.Offset, _56.Limit), _56.Next, client) :
            op is InfusioOp<T>.CreateOpportunity _57 ? Exe(() => client.CreateOpportunity(_57.Opportunity), _57.Next, client) :
            op is InfusioOp<T>.UpdateOpportunity _58 ? Exe(() => client.UpdateOpportunity(_58.Opportunity), _58.Next, client) :
            op is InfusioOp<T>.RetrieveOpportunityModel _59 ? Exe(() => client.RetrieveOpportunityModel(), _59.Next, client) :
            op is InfusioOp<T>.GetOpportunity _60 ? Exe(() => client.GetOpportunity(_60.OpportunityId, _60.OptionalProperties), _60.Next, client) :
            op is InfusioOp<T>.UpdatePropertiesOnOpportunity _61 ? Exe(() => client.UpdatePropertiesOnOpportunity(_61.OpportunityId, _61.Opportunity), _61.Next, client) :
            op is InfusioOp<T>.ListOpportunityStagePipelines _62 ? Exe(() => client.ListOpportunityStagePipelines(), _62.Next, client) :
            op is InfusioOp<T>.ListOrders _63 ? Exe(() => client.ListOrders(_63.ProductId, _63.ContactId, _63.Order, _63.Paid, _63.Offset, _63.Limit, _63.Until, _63.Since), _63.Next, client) :
            op is InfusioOp<T>.RetrieveOrderModel _64 ? Exe(() => client.RetrieveOrderModel(), _64.Next, client) :
            op is InfusioOp<T>.GetOrder _65 ? Exe(() => client.GetOrder(_65.OrderId), _65.Next, client) :
            op is InfusioOp<T>.ListTransactionsForOrder _66 ? Exe(() => client.ListTransactionsForOrder(_66.OrderId, _66.ContactId, _66.Offset, _66.Limit, _66.Until, _66.Since), _66.Next, client) :
            op is InfusioOp<T>.ListProducts _67 ? Exe(() => client.ListProducts(_67.Active, _67.Offset, _67.Limit), _67.Next, client) :
            op is InfusioOp<T>.ListProductsFromSyncToken _68 ? Exe(() => client.ListProductsFromSyncToken(_68.Offset, _68.Limit, _68.SyncToken), _68.Next, client) :
            op is InfusioOp<T>.GetProduct _69 ? Exe(() => client.GetProduct(_69.ProductId), _69.Next, client) :
            op is InfusioOp<T>.GetApplicationEnabled _70 ? Exe(() => client.GetApplicationEnabled(), _70.Next, client) :
            op is InfusioOp<T>.GetContactOptionTypes _71 ? Exe(() => client.GetContactOptionTypes(), _71.Next, client) :
            op is InfusioOp<T>.RetrieveSubscriptionModel _72 ? Exe(() => client.RetrieveSubscriptionModel(), _72.Next, client) :
            op is InfusioOp<T>.ListTags _73 ? Exe(() => client.ListTags(_73.Category, _73.Offset, _73.Limit), _73.Next, client) :
            op is InfusioOp<T>.CreateTag _74 ? Exe(() => client.CreateTag(_74.Tag), _74.Next, client) :
            op is InfusioOp<T>.CreateTagCategory _75 ? Exe(() => client.CreateTagCategory(_75.TagCategory), _75.Next, client) :
            op is InfusioOp<T>.GetTag _76 ? Exe(() => client.GetTag(_76.Id), _76.Next, client) :
            op is InfusioOp<T>.ListContactsForTagId _77 ? Exe(() => client.ListContactsForTagId(_77.TagId, _77.Offset, _77.Limit), _77.Next, client) :
            op is InfusioOp<T>.ApplyTagToContactIds _78 ? Exe(() => client.ApplyTagToContactIds(_78.Ids, _78.TagId), _78.Next, client) :
            op is InfusioOp<T>.RemoveTagFromContactIds _79 ? Exe(() => client.RemoveTagFromContactIds(_79.Ids, _79.TagId), _79.Next, client) :
            op is InfusioOp<T>.RemoveTagFromContactId _80 ? Exe(() => client.RemoveTagFromContactId(_80.ContactId, _80.TagId), _80.Next, client) :
            op is InfusioOp<T>.ListTasks _81 ? Exe(() => client.ListTasks(_81.Order, _81.Offset, _81.Limit, _81.Completed, _81.Until, _81.Since, _81.UserId, _81.HasDueDate, _81.ContactId), _81.Next, client) :
            op is InfusioOp<T>.CreateTask _82 ? Exe(() => client.CreateTask(_82.Task), _82.Next, client) :
            op is InfusioOp<T>.RetrieveTaskModel _83 ? Exe(() => client.RetrieveTaskModel(), _83.Next, client) :
            op is InfusioOp<T>.ListTasksForCurrentUser _84 ? Exe(() => client.ListTasksForCurrentUser(_84.Order, _84.Offset, _84.Limit, _84.Completed, _84.Until, _84.Since, _84.UserId, _84.HasDueDate, _84.ContactId), _84.Next, client) :
            op is InfusioOp<T>.GetTask _85 ? Exe(() => client.GetTask(_85.TaskId), _85.Next, client) :
            op is InfusioOp<T>.UpdateTask _86 ? Exe(() => client.UpdateTask(_86.Task, _86.TaskId), _86.Next, client) :
            op is InfusioOp<T>.DeleteTask _87 ? Exe(() => client.DeleteTask(_87.TaskId), _87.Next, client) :
            op is InfusioOp<T>.UpdatePropertiesOnTask _88 ? Exe(() => client.UpdatePropertiesOnTask(_88.Task, _88.TaskId), _88.Next, client) :
            op is InfusioOp<T>.ListTransactions _89 ? Exe(() => client.ListTransactions(_89.ContactId, _89.Offset, _89.Limit, _89.Until, _89.Since), _89.Next, client) :
            op is InfusioOp<T>.GetTransaction _90 ? Exe(() => client.GetTransaction(_90.TransactionId), _90.Next, client) :
            throw new NotSupportedException();

        static Task<Either<Error, B>> Exe<T, B>(Func<Task<Either<Error, T>>> fn, Func<T, InfusioOp<B>> nextOp, InfusioClient client) =>
            from profile in fn()
            from next in RunAsync(nextOp(profile), client)
            select next;
    }
}