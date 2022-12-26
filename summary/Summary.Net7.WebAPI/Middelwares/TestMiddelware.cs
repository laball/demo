namespace Summary.Net7.WebAPI.Middelwares
{
    public class TestMiddelware
    {
        private readonly RequestDelegate _next;

        public TestMiddelware(RequestDelegate next)
        {
            this._next = next;
        }
        public Task Invoke(HttpContext context)
        {
            if (context?.Request?.Path.Value?.Contains("1.jpg") == true)
            {
                return context.Response.SendFileAsync("1.jpg");
            }

            if (this._next != null)
            {
                return this._next(context);
            }

            throw new Exception("TestMiddelware error");
        }
    }
}
