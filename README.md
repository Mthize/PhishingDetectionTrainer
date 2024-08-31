# Phishing Detection Trainer

The **Phishing Detection Trainer** is a Windows Forms application designed to help users practice identifying phishing attempts in emails. This tool simulates various phishing scenarios with different difficulty levels, providing users with feedback on their detection skills.

## Features

- **Simulated Phishing Scenarios:** Present various phishing email scenarios with different characteristics.
- **Difficulty Levels:** Choose from Easy, Medium, and Hard levels to adjust the challenge.
- **Scoring System:** Track and display user performance based on the accuracy of identified phishing flags.
- **Countdown Timer:** Manage the time allocated for each scenario based on the selected difficulty level.
- **Interactive Feedback:** Buttons to identify specific phishing characteristics with visual feedback.

## Getting Started

### Prerequisites

- .NET Framework (version compatible with Windows Forms)
- Visual Studio or any compatible IDE

### Installation

1. Clone the repository or download the code files.
2. Open the project in Visual Studio.
3. Build and run the project.

## Code Explanation

### Class: `MainForm`

This is the primary form of the application where most of the interaction occurs.

#### Constructor: `MainForm()`

- Initializes the form components.
- Sets up phishing scenarios.
- Initializes the timer.
- Starts a new training session.

#### Method: `InitializeComponent()`

Sets up the layout and controls of the form:
- `richTextBoxContent` for displaying the email content.
- `labelSubject` for showing the email subject.
- `labelTimer` for displaying the remaining time.
- Buttons (`buttonSuspiciousURL`, `buttonUrgency`, `buttonGenericGreeting`, `buttonNext`) for user interaction.
- `comboBoxDifficulty` for selecting the difficulty level.

#### Method: `InitializeScenarios()`

Creates and initializes a list of `PhishingScenario` objects with predefined subjects, contents, red flags, and difficulty levels.

#### Method: `InitializeTimer()`

Sets up the timer with a 1-second interval, triggers `Timer_Tick` on each tick.

#### Method: `Timer_Tick(object sender, EventArgs e)`

Handles timer updates, decreases the remaining time, and advances to the next scenario when the time runs out.

#### Method: `StartNewTrainingSession()`

Resets the session state, shuffles scenarios, and displays the first scenario.

#### Method: `ShuffleScenarios()`

Randomly shuffles the order of scenarios to ensure varied practice.

#### Method: `DisplayCurrentScenario()`

Displays the current scenario's subject and content, resets the flag buttons, and starts the timer.

#### Method: `ResetFlagButtons()`

Resets the state and appearance of the flag buttons.

#### Method: `ResetTimer()`

Sets the timer duration based on the current difficulty level and starts the timer.

#### Method: `GetTimerDuration()`

Returns the timer duration based on the selected difficulty level.

#### Method: `OnFlagIdentified(object sender, EventArgs e)`

Handles button clicks for identifying phishing flags, updates the score, and provides visual feedback.

#### Method: `OnNextScenario(object sender, EventArgs e)`

Advances to the next scenario and resets the timer.

#### Method: `ShowResults()`

Displays the user's final score and difficulty level, then starts a new training session.

#### Method: `OnDifficultyChanged(object sender, EventArgs e)`

Changes the difficulty level based on the user's selection and restarts the training session.

### Class: `PhishingScenario`

Represents a phishing email scenario.

#### Properties

- `Subject`: The email subject.
- `Content`: The email content.
- `RedFlags`: List of phishing characteristics present in the scenario.
- `Difficulty`: Difficulty level of the scenario.

#### Constructor

Initializes a new phishing scenario with the provided subject, content, red flags, and difficulty level.

### Enum: `DifficultyLevel`

Defines the difficulty levels for the scenarios:
- `Easy`
- `Medium`
- `Hard`

## Usage

1. **Select Difficulty:** Choose the desired difficulty level from the dropdown menu.
2. **Identify Flags:** Analyze the email content and click on the appropriate buttons to identify suspicious characteristics.
3. **Advance to Next Scenario:** Click "Next" to proceed to the next scenario after the time or user choice.
4. **View Results:** After completing all scenarios, view your score and performance results.

## Contribution

Feel free to contribute to the project by adding new scenarios or improving the code. Pull requests and issues are welcome!
