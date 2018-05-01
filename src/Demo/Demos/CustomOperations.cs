using Infusio;
using Infusio.Model;
using LanguageExt;

namespace Demo.Demos
{
    using static Prelude;
    using static InfusioDsl;

    public static class CustomOperations
    {
        public static InfusioOp<FullContact> AddTagToContact(Tag tag, EmailAddress email) =>
            from contact in GetOrCreateContact(email)
            from remoteTag in GetOrCreateTag(tag)
            from _ in ApplyTagsToContactId(new TagId(List(remoteTag.Id ?? 0)), contact.Id ?? 0)
            select contact.Copy(tagIds: contact.TagIds.Add(remoteTag.Id ?? 0));

        static InfusioOp<Tag> GetOrCreateTag(Tag tag) =>
            from tags in ListTags()
            from t in tags.InnerTags
                .Find(x => x.Name == tag.Name)
                .Map(Return)
                .IfNone(CreateNewTag(tag))
            select t;

        static InfusioOp<Tag> CreateNewTag(Tag tag)
        {
            var input = new CreateTag(name: tag.Name, description: tag.Description);
            return CreateTag(
                Optional(tag.Category)
                    .Map(cat => input.Copy(category: new CategoryReference(cat.Id)))
                    .IfNone(input)
            );
        }

        static InfusioOp<FullContact> GetOrCreateContact(EmailAddress address) =>
            from contacts in ListContacts(email: address.Email)
            from c in contacts.Contacts
                .HeadOrNone()
                .Map(Return)
                .IfNone(CreateContact(new RequestContact(emailAddresses: List(address))))
            select c;
    }
}