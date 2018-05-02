using Infusio;
using Infusio.Model;
using LanguageExt;

namespace Demo
{
    using static Prelude;
    using static InfusioDsl;

    public static class CustomOperations
    {
        public static InfusioOp<FullContact> AddTagToContact(Tag tag, EmailAddress email) =>
            from l1 in Log($"Adding tag '{tag.Name}' to contact '{email.Email}'")
            from contact in GetOrCreateContact(email)
            from remoteTag in GetOrCreateTag(tag)
            from _ in ApplyTagsToContactId(new TagId(List(remoteTag.Id ?? 0)), contact.Id ?? 0)
            select contact.Copy(tagIds: contact.TagIds.Add(remoteTag.Id ?? 0));

        static InfusioOp<Tag> GetOrCreateTag(Tag tag) =>
            from tags in ListTags()
            from t in tags.InnerTags
                .Find(x => x.Name == tag.Name)
                .Map(Return)
                .IfNone(CreateTag(new CreateTag(name: tag.Name, description: tag.Description)))
            select t;

        static InfusioOp<FullContact> GetOrCreateContact(EmailAddress address) =>
            from contacts in ListContacts(email: address.Email)
            from c in contacts.Contacts
                .HeadOrNone()
                .Map(Return)
                .IfNone(CreateContact(new RequestContact(emailAddresses: List(address))))
            select c;

        static InfusioOp<FullContact> AddTagToContact2(Tag tag, EmailAddress email) =>
            Log($"Adding tag '{tag.Name}' to contact '{email.Email}'")
                .SelectMany(_ => GetOrCreateContact(email), (x, contact) => contact)
                .SelectMany(contact => GetOrCreateTag(tag), (contact, t) => new {contact, tag = t})
                .SelectMany(t => ApplyTagsToContactId(new TagId(List(t.tag.Id ?? 0)), t.contact.Id ?? 0),
                    (t, _) => t.contact.Copy(tagIds: t.contact.TagIds.Add(t.tag.Id ?? 0)));
    }
}