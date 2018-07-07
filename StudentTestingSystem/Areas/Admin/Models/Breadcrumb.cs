namespace StudentTestingSystem.Areas.Admin.Models
{
    public class Breadcrumb
    {
        private readonly string _title;
        private readonly string _action;
        private readonly string _controller;
        private readonly object _routeValues;

        public Breadcrumb(string title)
        {
            _title = title;
            _action = null;
            _controller = null;
            _routeValues = null;
        }

        public Breadcrumb(string title, string action, string controller, object routeValues = null)
        {
            _title = title;
            _action = action;
            _controller = controller;
            _routeValues = routeValues;
        }

        public string Title { get => _title; }
        public string Action { get => _action; }
        public string Controller { get => _controller; }
        public object RouteValues { get => _routeValues; }
    }
}