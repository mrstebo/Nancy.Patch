namespace Nancy.Patch
{
    public class PatchResult
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }

        public static implicit operator bool(PatchResult result)
        {
            return result.Succeeded;
        }
    }
}
