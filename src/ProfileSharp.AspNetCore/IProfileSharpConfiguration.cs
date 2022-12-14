namespace ProfileSharp.AspNetCore
{
    public interface IProfileSharpConfiguration
    {
        void EnableProfiling();

        void EnableMocking();

        void Disable();
    }
}
