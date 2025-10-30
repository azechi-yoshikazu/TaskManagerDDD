using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.DomainErrors
{
    public static class UserErrors
    {
        public static readonly Error DisplayNameIsRequired = new Error("User.DisplayName.IsRequired", "表示名は必須です。");

        public static readonly Error EmailIsRequired = new Error("User.Email.IsRequired", "メールアドレスは必須です。");
        public static readonly Error EmailInvalidFormat = new Error("User.Email.InvalidFormat", "メールアドレスの形式が正しくありません。");
    }
}
