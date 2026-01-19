using ContactBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ContactBook.UnitTests.Core.Domain.Entities
{
    public class ContactsTests
    {
        [Fact]
        public void TestIfAddContactWorks()
        {
            Contact contact = new("Louis Armstrong", "louis@gmail.com", "81998765432");

            Assert.NotNull(contact.Name);
            Assert.NotEmpty(contact.Name);

            Assert.NotNull(contact.Email);
            Assert.NotEmpty(contact.Email);

            Assert.NotNull(contact.Phone);
            Assert.NotEmpty(contact.Phone);
        }
        public void TestIfUpdateContactWorks()
        {
            Contact contact = new("Louis Armstrong", "louis@gmail.com", "81998765432");

            Assert.NotNull(contact.Name);
            Assert.NotEmpty(contact.Name);

            Assert.NotNull(contact.Email);
            Assert.NotEmpty(contact.Email);

            Assert.NotNull(contact.Phone);
            Assert.NotEmpty(contact.Phone);

            string oldName = contact.Name;

            contact.Update("Louis Armweak", contact.Email, contact.Phone);

            Assert.NotNull(contact.Name);
            Assert.NotEqual(contact.Name, oldName);

            Assert.NotNull(contact.Email);
            Assert.NotEmpty(contact.Email);

            Assert.NotNull(contact.Phone);
            Assert.NotEmpty(contact.Phone);
        }


    }
}
