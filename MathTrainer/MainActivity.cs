using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MathTrainer
{
	[Activity(Label = "MathTrainer", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int test_count = 0;
		int score = 0;

		Random rand = new Random();
		int a, b;

		protected void NextTest()
		{
			TextView operation_view = FindViewById<TextView>(Resource.Id.operation);
			a = rand.Next(1, 10);
			b = rand.Next(1, 10);
			operation_view.Text = a + " x " + b;
			++test_count;

			NumberPicker answer_picker = FindViewById<NumberPicker>(Resource.Id.answer_picker);
			answer_picker.Value = 0;
		}

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Start first test
			NextTest();

			// Get our button from the layout resource,
			// and attach an event to it
			NumberPicker answer_picker = FindViewById<NumberPicker>(Resource.Id.answer_picker);
			answer_picker.MinValue = 0;
			answer_picker.MaxValue = 100;

			// Loop on answer
			answer_picker.ValueChanged += delegate {
				int answer = answer_picker.Value;
				if (answer == a * b)
					++score;

				TextView score_view = FindViewById<TextView>(Resource.Id.score);
				score_view.Text = score + " / " + test_count;

				NextTest();
			};
		}
	}
}

