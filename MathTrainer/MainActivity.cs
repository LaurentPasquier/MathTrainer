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

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Start first test
			NextTest();

			// Get buttons from the layout resource and attach event to each of them
			FindViewById<Button>(Resource.Id.button_0).Click += delegate { AppendDigit("0"); };
			FindViewById<Button>(Resource.Id.button_1).Click += delegate { AppendDigit("1"); };
			FindViewById<Button>(Resource.Id.button_2).Click += delegate { AppendDigit("2"); };
			FindViewById<Button>(Resource.Id.button_3).Click += delegate { AppendDigit("3"); };
			FindViewById<Button>(Resource.Id.button_4).Click += delegate { AppendDigit("4"); };
			FindViewById<Button>(Resource.Id.button_5).Click += delegate { AppendDigit("5"); };
			FindViewById<Button>(Resource.Id.button_6).Click += delegate { AppendDigit("6"); };
			FindViewById<Button>(Resource.Id.button_7).Click += delegate { AppendDigit("7"); };
			FindViewById<Button>(Resource.Id.button_8).Click += delegate { AppendDigit("8"); };
			FindViewById<Button>(Resource.Id.button_9).Click += delegate { AppendDigit("9"); };
			FindViewById<Button>(Resource.Id.button_BckSpc).Click += delegate { BackSpace(); };
			FindViewById<Button>(Resource.Id.button_Enter).Click += delegate { CheckAnswer(); };
		}

		protected void NextTest()
		{
			TextView operation_view = FindViewById<TextView>(Resource.Id.operation);
			a = rand.Next(1, 10);
			b = rand.Next(1, 10);
			operation_view.Text = a + " x " + b;
			++test_count;

			EditText answer_text = FindViewById<EditText>(Resource.Id.editText_answer);
			answer_text.Text = "0";
			answer_text.Selected = true;
		}

		protected void AppendDigit(string digit)
		{
			EditText answer_text = FindViewById<EditText>(Resource.Id.editText_answer);
			if (answer_text.Text != "0")
				answer_text.Text += digit[0];
			else
				answer_text.Text = digit;
		}

		protected void BackSpace()
		{
			EditText answer_text = FindViewById<EditText>(Resource.Id.editText_answer);
			answer_text.Text = "0";
		}

		protected void CheckAnswer()
		{
			UInt32 answer = 0;
			EditText answer_text = FindViewById<EditText>(Resource.Id.editText_answer);
			if (UInt32.TryParse(answer_text.Text, out answer))
			{
				if (answer == a * b)
					++score;
				TextView score_view = FindViewById<TextView>(Resource.Id.score);
				score_view.Text = score + " / " + test_count;
				NextTest();
			}
		}

	}
}

