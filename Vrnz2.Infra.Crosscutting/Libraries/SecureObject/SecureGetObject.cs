using System;

namespace Vrnz2.Infra.CrossCutting.Libraries.SecureObject
{
    public class SecureGetObject<T>
    {
        #region Variables

        public static readonly SecureGetObject<T> Empty = new SecureGetObject<T>();

        #endregion

        #region Constructors

        public SecureGetObject(T value)
        {
            Value = value;
            IsEmpty = false;
        }

        private SecureGetObject()
            => IsEmpty = true;

        #endregion

        #region Attributes

        public T Value { get; }

        public bool IsEmpty { get; }

        public bool HasValue => !IsEmpty;

        #endregion

        #region Methods

        public SecureGetObject<TResult> Bind<TResult>(Func<T, SecureGetObject<TResult>> f)
            => IsEmpty ? SecureGetObject<TResult>.Empty : f(Value);

        #endregion
    }
}
