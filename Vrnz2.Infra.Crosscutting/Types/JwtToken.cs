using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Vrnz2.Infra.CrossCutting.Extensions;

namespace Vrnz2.Infra.CrossCutting.Types
{
    public struct JwtToken
    {
        #region Atributes

        public string OriginalValue { get; private set; }

        public string Value { get; private set; }

        public bool IsValid { get; private set; }

        public bool IsJwtToken { get; private set; }

        public DateTimeOffset? ExpirationDate { get; private set; }

        #endregion

        #region Constructors

        public JwtToken(string token)
            : this()
            => IsValid = JwtIsValid(token);

        public JwtToken(string userId, string ip)
            : this()
        {
        
        }

        #endregion

        #region Operator

        public static implicit operator JwtToken(string value)
            => new JwtToken(value);

        public static implicit operator JwtToken((string UserId, string Ip) fullName)
            => new JwtToken(fullName.UserId, fullName.Ip);

        #endregion

        #region Methods

        public bool IsEmpty()
            => string.IsNullOrWhiteSpace(Value);

        public bool IsNull()
            => Value.IsNull();

        #endregion

        #region methods

        //public string CriarToken(string userId, string ip)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();

        //    using (var asym_key = new AsymmetricKey())
        //    {
        //        signature = asym_key.GetJwtSignature(p_id, u_id, rnd);

        //        if (!asym_key.JwtSignatureIsValid(p_id, u_id, rnd, signature))
        //            throw new Exception("Erro na verificação da assinatura do Token de acesso!");

        //        auth_valid_data_str = asym_key.Encrypt(auth_valid_data.ToJson());
        //    }

        //    var claimsIdentity = new ClaimsIdentity(new[]
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, userId),
        //        new Claim("u_id", userId),
        //        new Claim("signature", signature),
        //    });

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = claimsIdentity,
        //        Issuer = this._sec_cfg.TokenIssuer,
        //        Audience = this._sec_cfg.TokenAudience,
        //        SigningCredentials = this._sec_cfg.SigningCredentials,
        //        IssuedAt = now,
        //        Expires = now.AddMinutes(this._sec_cfg.TokenLifetimeInMinutes)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);

        //    return tokenHandler.WriteToken(token);
        //}

        public bool JwtIsValid(string token)
        {
            OriginalValue = token;

            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(OriginalValue);

            IsJwtToken = jwtToken.IsNotNull();

            IsValid = IsJwtToken && jwtToken.ValidFrom <= DateTimeOffset.UtcNow && DateTimeOffset.UtcNow <= jwtToken.ValidTo;

            if (!IsValid) ExpirationDate = jwtToken?.ValidTo;

            if (IsValid) Value = OriginalValue;

            return IsValid;
        }

        #endregion
    }
}
