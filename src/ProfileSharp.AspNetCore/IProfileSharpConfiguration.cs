namespace ProfileSharp.AspNetCore
{
    public interface IProfileSharpConfiguration
    {
        void UseProfiling();

        void UseMocking();

        void Disable();
    }
}
