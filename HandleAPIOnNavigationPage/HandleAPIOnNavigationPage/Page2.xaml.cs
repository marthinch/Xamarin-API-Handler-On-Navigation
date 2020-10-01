using System;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HandleAPIOnNavigationPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page2 : ContentPage
    {
        private CancellationTokenSource cancellationTokenSource;

        public Page2()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Console.WriteLine("Start call api on page 2 at {0}", DateTime.Now);

            var result = await CallAPI().ConfigureAwait(false);
            Console.WriteLine("Result {0}", result);

            Console.WriteLine("Finish call api on page 2 at {0}", DateTime.Now);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            cancellationTokenSource.Cancel();
        }

        private Task<string> CallAPI()
        {
            cancellationTokenSource = new CancellationTokenSource();

            return Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        Thread.Sleep(10000);

                        if (cancellationTokenSource.IsCancellationRequested)
                            cancellationTokenSource.Token.ThrowIfCancellationRequested();

                        return "RESULT OF API FOR PAGE 2";
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Cancel call api on page 2 at {0}", DateTime.Now);
                }

                return null;

            }, cancellationTokenSource.Token);
        }
    }
}