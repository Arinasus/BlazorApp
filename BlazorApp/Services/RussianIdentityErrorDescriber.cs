using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace BlazorApp.Services
{
    public class RussianIdentityErrorDescriber : IdentityErrorDescriber
    {
        private readonly IStringLocalizer<RussianIdentityErrorDescriber> _localizer;

        public RussianIdentityErrorDescriber(IStringLocalizer<RussianIdentityErrorDescriber> localizer)
        {
            _localizer = localizer;
        }

        public override IdentityError InvalidUserName(string userName)
            => new IdentityError { Code = nameof(InvalidUserName), Description = _localizer["InvalidUserName", userName] };

        public override IdentityError PasswordMismatch()
            => new IdentityError { Code = nameof(PasswordMismatch), Description = _localizer["PasswordMismatch"] };

        public override IdentityError PasswordTooShort(int length)
            => new IdentityError { Code = nameof(PasswordTooShort), Description = _localizer["PasswordTooShort", length] };
    }
}
