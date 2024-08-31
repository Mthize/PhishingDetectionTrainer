using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PhishingDetectionTrainer
{
    public partial class MainForm : Form
    {
        private List<PhishingScenario> scenarios;
        private int currentScenarioIndex;
        private int score;
        private Timer timer;
        private int timeRemaining;
        private Random random = new Random();
        private DifficultyLevel currentDifficulty = DifficultyLevel.Easy;

        public MainForm()
        {
            InitializeComponent();
            InitializeScenarios();
            InitializeTimer();
            StartNewTrainingSession();
        }

        private void InitializeComponent()
        {
            this.richTextBoxContent = new System.Windows.Forms.RichTextBox();
            this.labelSubject = new System.Windows.Forms.Label();
            this.labelTimer = new System.Windows.Forms.Label();
            this.buttonSuspiciousURL = new System.Windows.Forms.Button();
            this.buttonUrgency = new System.Windows.Forms.Button();
            this.buttonGenericGreeting = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.comboBoxDifficulty = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // richTextBoxContent
            // 
            this.richTextBoxContent.Location = new System.Drawing.Point(12, 70);
            this.richTextBoxContent.Name = "richTextBoxContent";
            this.richTextBoxContent.Size = new System.Drawing.Size(776, 300);
            this.richTextBoxContent.TabIndex = 0;
            this.richTextBoxContent.Text = "";
            // 
            // labelSubject
            // 
            this.labelSubject.AutoSize = true;
            this.labelSubject.Location = new System.Drawing.Point(12, 9);
            this.labelSubject.Name = "labelSubject";
            this.labelSubject.Size = new System.Drawing.Size(55, 17);
            this.labelSubject.TabIndex = 1;
            this.labelSubject.Text = "Subject";
            // 
            // labelTimer
            // 
            this.labelTimer.AutoSize = true;
            this.labelTimer.Location = new System.Drawing.Point(715, 9);
            this.labelTimer.Name = "labelTimer";
            this.labelTimer.Size = new System.Drawing.Size(73, 17);
            this.labelTimer.TabIndex = 2;
            this.labelTimer.Text = "Time: 60s";
            // 
            // buttonSuspiciousURL
            // 
            this.buttonSuspiciousURL.Location = new System.Drawing.Point(12, 376);
            this.buttonSuspiciousURL.Name = "buttonSuspiciousURL";
            this.buttonSuspiciousURL.Size = new System.Drawing.Size(150, 30);
            this.buttonSuspiciousURL.TabIndex = 3;
            this.buttonSuspiciousURL.Text = "Suspicious URL";
            this.buttonSuspiciousURL.UseVisualStyleBackColor = true;
            this.buttonSuspiciousURL.Click += new System.EventHandler(this.OnFlagIdentified);
            // 
            // buttonUrgency
            // 
            this.buttonUrgency.Location = new System.Drawing.Point(168, 376);
            this.buttonUrgency.Name = "buttonUrgency";
            this.buttonUrgency.Size = new System.Drawing.Size(150, 30);
            this.buttonUrgency.TabIndex = 4;
            this.buttonUrgency.Text = "Urgency";
            this.buttonUrgency.UseVisualStyleBackColor = true;
            this.buttonUrgency.Click += new System.EventHandler(this.OnFlagIdentified);
            // 
            // buttonGenericGreeting
            // 
            this.buttonGenericGreeting.Location = new System.Drawing.Point(324, 376);
            this.buttonGenericGreeting.Name = "buttonGenericGreeting";
            this.buttonGenericGreeting.Size = new System.Drawing.Size(150, 30);
            this.buttonGenericGreeting.TabIndex = 5;
            this.buttonGenericGreeting.Text = "Generic Greeting";
            this.buttonGenericGreeting.UseVisualStyleBackColor = true;
            this.buttonGenericGreeting.Click += new System.EventHandler(this.OnFlagIdentified);
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(638, 376);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(150, 30);
            this.buttonNext.TabIndex = 6;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.OnNextScenario);
            // 
            // comboBoxDifficulty
            // 
            this.comboBoxDifficulty.FormattingEnabled = true;
            this.comboBoxDifficulty.Items.AddRange(new object[] { "Easy", "Medium", "Hard" });
            this.comboBoxDifficulty.Location = new System.Drawing.Point(12, 40);
            this.comboBoxDifficulty.Name = "comboBoxDifficulty";
            this.comboBoxDifficulty.Size = new System.Drawing.Size(121, 24);
            this.comboBoxDifficulty.TabIndex = 7;
            this.comboBoxDifficulty.SelectedIndexChanged += new System.EventHandler(this.OnDifficultyChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comboBoxDifficulty);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonGenericGreeting);
            this.Controls.Add(this.buttonUrgency);
            this.Controls.Add(this.buttonSuspiciousURL);
            this.Controls.Add(this.labelTimer);
            this.Controls.Add(this.labelSubject);
            this.Controls.Add(this.richTextBoxContent);
            this.Name = "MainForm";
            this.Text = "Phishing Detection Trainer";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.RichTextBox richTextBoxContent;
        private System.Windows.Forms.Label labelSubject;
        private System.Windows.Forms.Label labelTimer;
        private System.Windows.Forms.Button buttonSuspiciousURL;
        private System.Windows.Forms.Button buttonUrgency;
        private System.Windows.Forms.Button buttonGenericGreeting;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.ComboBox comboBoxDifficulty;

        private void InitializeScenarios()
        {
            scenarios = new List<PhishingScenario>
            {
                new PhishingScenario(
                    "Urgent: Account Verification Required",
                    "Dear valued customer,\n\nWe have detected suspicious activity on your account. Please click the link below to verify your information immediately:\n\nhttp://secure-bank-verify.com\n\nFailure to do so may result in account suspension.\n\nBest regards,\nYour Bank",
                    new List<string> { "Suspicious URL", "Urgency", "Generic Greeting" },
                    DifficultyLevel.Easy
                ),
                new PhishingScenario(
                    "Important Update: Your Package Delivery",
                    "Hello,\n\nYour package could not be delivered due to an incorrect address. Please update your information within 24 hours to avoid return shipping fees:\n\nhttp://delivery-update-center.com/update\n\nThank you for your prompt attention to this matter.\n\nShipping Department",
                    new List<string> { "Suspicious URL", "Urgency" },
                    DifficultyLevel.Medium
                ),
                new PhishingScenario(
                    "Security Alert: Unusual Sign-In Activity",
                    "Dear User,\n\nWe've noticed an unusual sign-in attempt to your account from a new device. If this wasn't you, please secure your account immediately by clicking the link below:\n\nhttps://secure-account-verify.net/unusual-activity\n\nIf you don't take action within 2 hours, your account may be temporarily suspended for your protection.\n\nSecurity Team",
                    new List<string> { "Suspicious URL", "Urgency", "Generic Greeting" },
                    DifficultyLevel.Hard
                )
                // Add more scenarios here
            };
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeRemaining--;
            labelTimer.Text = $"Time: {timeRemaining}s";

            if (timeRemaining <= 0)
            {
                timer.Stop();
                OnNextScenario(sender, e);
            }
        }

        private void StartNewTrainingSession()
        {
            currentScenarioIndex = 0;
            score = 0;
            ShuffleScenarios();
            DisplayCurrentScenario();
        }

        private void ShuffleScenarios()
        {
            scenarios = scenarios.OrderBy(x => random.Next()).ToList();
        }

        private void DisplayCurrentScenario()
        {
            if (currentScenarioIndex < scenarios.Count)
            {
                PhishingScenario scenario = scenarios[currentScenarioIndex];
                labelSubject.Text = $"Subject: {scenario.Subject}";
                richTextBoxContent.Text = scenario.Content;

                ResetFlagButtons();
                ResetTimer();
            }
            else
            {
                ShowResults();
            }
        }

        private void ResetFlagButtons()
        {
            buttonSuspiciousURL.Enabled = true;
            buttonSuspiciousURL.BackColor = SystemColors.Control;
            buttonUrgency.Enabled = true;
            buttonUrgency.BackColor = SystemColors.Control;
            buttonGenericGreeting.Enabled = true;
            buttonGenericGreeting.BackColor = SystemColors.Control;
        }

        private void ResetTimer()
        {
            timeRemaining = GetTimerDuration();
            labelTimer.Text = $"Time: {timeRemaining}s";
            timer.Start();
        }

        private int GetTimerDuration()
        {
            switch (currentDifficulty)
            {
                case DifficultyLevel.Easy:
                    return 60;
                case DifficultyLevel.Medium:
                    return 45;
                case DifficultyLevel.Hard:
                    return 30;
                default:
                    return 60;
            }
        }

        private void OnFlagIdentified(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string flag = clickedButton.Text;

            PhishingScenario currentScenario = scenarios[currentScenarioIndex];
            if (currentScenario.RedFlags.Contains(flag))
            {
                score++;
                clickedButton.BackColor = Color.Green;
            }
            else
            {
                clickedButton.BackColor = Color.Red;
            }

            clickedButton.Enabled = false;
        }

        private void OnNextScenario(object sender, EventArgs e)
        {
            timer.Stop();
            currentScenarioIndex++;
            DisplayCurrentScenario();
        }

        private void ShowResults()
        {
            timer.Stop();
            MessageBox.Show($"Training complete!\nYour score: {score}/{scenarios.Count}\nDifficulty: {currentDifficulty}", "Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            StartNewTrainingSession();
        }

        private void OnDifficultyChanged(object sender, EventArgs e)
        {
            currentDifficulty = (DifficultyLevel)comboBoxDifficulty.SelectedIndex;
            StartNewTrainingSession();
        }
    }

    public class PhishingScenario
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public List<string> RedFlags { get; set; }
        public DifficultyLevel Difficulty { get; set; }

        public PhishingScenario(string subject, string content, List<string> redFlags, DifficultyLevel difficulty)
        {
            Subject = subject;
            Content = content;
            RedFlags = redFlags;
            Difficulty = difficulty;
        }
    }

    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }
}