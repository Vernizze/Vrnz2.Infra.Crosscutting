namespace Vrnz2.Infra.CrossCutting.VOs.Extensions
{
    public class Diff
    {
        public static implicit operator Diff((string Base, string Compared) value)
            => new Diff(value.Base, value.Compared);

        public Diff(string @base, string compared)
        {
            Base = @base;
            Compared = compared;
        }

        public string Base { get; }
        public string Compared { get; }
    }
}
