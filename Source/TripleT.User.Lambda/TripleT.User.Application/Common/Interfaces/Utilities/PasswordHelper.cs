using System;
using TripleT.User.Application.Common.Utilities;
using TripleT.User.Domain.Domain;

namespace TripleT.User.Application.Common.Interfaces.Utilities
{
    public static class PasswordHelper
    {
        public static PasswordEntity CreatePassword(string password, string email)
        {
            var salt = Guid.NewGuid().ToString();
            var value = HashUtility.CreateSha256HashBase64String($"{password}{salt}");
            return new PasswordEntity(salt, value) { Email = email};
        }

        public static bool VerifyPassword(PasswordEntity passwordEntity, string password)
        {
            return passwordEntity.Value == HashUtility.CreateSha256HashBase64String($"{password}{passwordEntity.Salt}");
        }
    }
}