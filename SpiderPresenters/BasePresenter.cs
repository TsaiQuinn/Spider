using SpiderIView;

namespace SpiderPresenters
{
    public class BasePresenter<T> where T : IView
    {
        protected BasePresenter(T view)
        {
            View = view;
            ViewLoad();
        }

        public T View { get; set; }

        /// <summary>
        ///     加载视图后
        /// </summary>
        protected virtual void ViewLoad()
        {
        }
    }
}