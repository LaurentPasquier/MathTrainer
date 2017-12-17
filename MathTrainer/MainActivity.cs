using System;
using System.Collections.Generic;

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
		int test_index = 0;
		int score = 0;

		string default_input = "...";
		Random rand = new Random();
		List<int> selected_tables = new List<int>();
		int practice_length;
		int a, b;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

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
			FindViewById<Button>(Resource.Id.button_start20).Click += delegate { StartPractice(20); };
			FindViewById<Button>(Resource.Id.button_start50).Click += delegate { StartPractice(50); };
			FindViewById<Button>(Resource.Id.button_start100).Click += delegate { StartPractice(100); };

			ToggleNumPad(false);
			ToggleStartButtons(true);
		}

		protected void ToggleStartButtons(bool enable)
		{
			FindViewById<Button>(Resource.Id.button_start20).Enabled = enable;
			FindViewById<Button>(Resource.Id.button_start50).Enabled = enable;
			FindViewById<Button>(Resource.Id.button_start100).Enabled = enable;
		}

		protected void ToggleNumPad(bool enable)
		{
			FindViewById<Button>(Resource.Id.button_0).Enabled = enable;
			FindViewById<Button>(Resource.Id.button_1).Enabled = enable;
			FindViewById<Button>(Resource.Id.button_2).Enabled = enable;
			FindViewById<Button>(Resource.Id.button_3).Enabled = enable;
			FindViewById<Button>(Resource.Id.button_4).Enabled = enable;
			FindViewById<Button>(Resource.Id.button_5).Enabled = enable;
			FindViewById<Button>(Resource.Id.button_6).Enabled = enable;
			FindViewById<Button>(Resource.Id.button_7).Enabled = enable;
			FindViewById<Button>(Resource.Id.button_8).Enabled = enable;
			FindViewById<Button>(Resource.Id.button_9).Enabled = enable;
			FindViewById<Button>(Resource.Id.button_BckSpc).Enabled = enable;
			FindViewById<Button>(Resource.Id.button_Enter).Enabled = enable;
		}

		protected void StartPractice(int new_practice_length)
		{
			practice_length = new_practice_length;
			SetupSelectedTables();
			FindViewById<Chronometer>(Resource.Id.chronometer).Base = SystemClock.ElapsedRealtime();
			FindViewById<Chronometer>(Resource.Id.chronometer).Start();

			// Start first test
			score = 0;
			test_index = 0;
			PrintScore();
			NextTest();

			// Disable start buttons and enable enter button
			ToggleNumPad(true);
			ToggleStartButtons(false);
		}

		protected void SetupSelectedTables()
		{
			selected_tables.Clear();
			if (FindViewById<CheckBox>(Resource.Id.checkBox_0).Checked) selected_tables.Add(0);
			if (FindViewById<CheckBox>(Resource.Id.checkBox_1).Checked) selected_tables.Add(1);
			if (FindViewById<CheckBox>(Resource.Id.checkBox_2).Checked) selected_tables.Add(2);
			if (FindViewById<CheckBox>(Resource.Id.checkBox_3).Checked) selected_tables.Add(3);
			if (FindViewById<CheckBox>(Resource.Id.checkBox_4).Checked) selected_tables.Add(4);
			if (FindViewById<CheckBox>(Resource.Id.checkBox_5).Checked) selected_tables.Add(5);
			if (FindViewById<CheckBox>(Resource.Id.checkBox_6).Checked) selected_tables.Add(6);
			if (FindViewById<CheckBox>(Resource.Id.checkBox_7).Checked) selected_tables.Add(7);
			if (FindViewById<CheckBox>(Resource.Id.checkBox_8).Checked) selected_tables.Add(8);
			if (FindViewById<CheckBox>(Resource.Id.checkBox_9).Checked) selected_tables.Add(9);
			if (FindViewById<CheckBox>(Resource.Id.checkBox_10).Checked) selected_tables.Add(10);
		}

		protected void PrintScore()
		{
			TextView score_view = FindViewById<TextView>(Resource.Id.score);
			score_view.Text = score + " / " + test_index + " ( / " + practice_length + " )   ";
		}

		protected void NextTest()
		{
			int table_count = selected_tables.Count;
			int index = rand.Next(0, table_count); // Next maxValue is exclusive !
			a = selected_tables[index];
			b = rand.Next(0, 11); // Next maxValue is exclusive !

			TextView operation_view = FindViewById<TextView>(Resource.Id.operation);
			operation_view.Text = a + " x " + b;
			++test_index;

			TextView answer_text = FindViewById<TextView>(Resource.Id.answer);
			answer_text.Text = default_input;
			answer_text.Selected = true;
		}

		protected void AppendDigit(string digit)
		{
			TextView answer_text = FindViewById<TextView>(Resource.Id.answer);
			if (answer_text.Text != default_input)
				answer_text.Text += digit[0];
			else
				answer_text.Text = digit;
		}

		protected void BackSpace()
		{
			TextView answer_text = FindViewById<TextView>(Resource.Id.answer);
			answer_text.Text = default_input;
		}

		protected void CheckAnswer()
		{
			UInt32 answer = 0;
			TextView answer_text = FindViewById<TextView>(Resource.Id.answer);
			if (UInt32.TryParse(answer_text.Text, out answer))
			{
				if (answer == a * b)
					++score;
				PrintScore();

				if (test_index < practice_length)
					NextTest();
				else
				{
					FindViewById<Chronometer>(Resource.Id.chronometer).Stop();
					FindViewById<TextView>(Resource.Id.operation).Text = default_input;
					FindViewById<TextView>(Resource.Id.answer).Text = default_input;
					ToggleNumPad(false);
					ToggleStartButtons(true);
				}
			}
		}

	}
}

