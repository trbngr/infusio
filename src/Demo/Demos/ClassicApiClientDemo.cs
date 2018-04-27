using System;
using System.Linq;
using System.Threading.Tasks;
using Infusio;
using Infusio.Http;
using Infusio.Model;
using LanguageExt;

namespace Demo.Demos
{
    using static Prelude;

    /// <summary>
    /// If you prefer programming against a classic api client, you can.
    /// As you can see, it's a lot more brittle.
    /// </summary>
    public static class ClassicApiClientDemo
    {
        public static async Task<Unit> Run(InfusioClient client)
        {
            var address = new EmailAddress(email: "chris@caliberweb.com");

            /*
             * The methods on client return a data type with two possible values.
             * Either<InfusioError, T>
             */
            var either = await client.ListContacts(email: address.Email);

            /*
             * Or you can use the unsafe versions which will throw exceptions as they occur
             */

            var list = await client.ListContactsUnsafe(email:address.Email);
            var contact = list.Contacts.FirstOrDefault();
            if (contact != null)
            {
                var tags = await client.ListTagsUnsafe();
                var remoteTag = tags.InnerTags.FirstOrDefault(x => x.Name == "developers");
                if (remoteTag == null)
                {
                    remoteTag = await client.CreateTagUnsafe(new CreateTag(name: "developers"));
                }

                await client.ApplyTagsToContactIdUnsafe(new TagId(List(remoteTag.Id ?? 0)), contact.Id ?? 0);
                contact = contact.Copy(tagIds: contact.TagIds.Add(remoteTag.Id ?? 0));
            }

            /*
             * Of course, you can use the client in a monadic manner as well.
             * One of the differences between this and the DSL is that the DSL is lazy and only
             * constructs somewhat of a syntax tree. You can also interpret the DSL anyway you please.
             * You can't do that with the client. It's executed always.
             */

//            var clientProgram =
//                from c in client.ListContacts(email: address.Email).ToAsync().Match(
//                    Left: _ => Left<InfusioError, FullContact>(_),
//                    Right: x => x.Contacts.HeadOrNone().Map(identity).IfNone(() =>
//
//                        I give up ;)

//                        client.CreateContact(new RequestContact(emailAddresses: List(address)))
//                        )
//                )


            return unit;
        }
    }
}