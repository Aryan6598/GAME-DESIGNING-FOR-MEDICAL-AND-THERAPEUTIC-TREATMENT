#include <iostream>
using namespace std;

// Function to ask questions and return the score
int askQuestion(string question, string options[], int numOptions) {
    int choice;
    cout << question << endl;
    for (int i = 0; i < numOptions; ++i) {
        cout << i + 1 << ". " << options[i] << endl;
    }
    cout << "Please select an option (1-" << numOptions << "): ";
    cin >> choice;

    while (choice < 1 || choice > numOptions) {
        cout << "Invalid input. Please select a valid option (1-" << numOptions << "): ";
        cin >> choice;
    }

    return choice; // Returning the choice (points based on answer)
}

// Function to calculate improvement percentage
double calculateImprovement(int beforeScore, int afterScore) {
    if (beforeScore == 0) {
        return 0.0; // Avoid division by zero
    }
    return ((afterScore - beforeScore) / (double)beforeScore) * 100;
}

int main() {
    int beforeTotal = 0, afterTotal = 0;

    // Questions and answer options
    string questions[] = {
        "How would you rate your reaction time while responding to sudden stimuli (e.g., catching a falling object)?",
        "How often do you miss or fail to respond to quick-moving objects?",
        "How would you rate your hand-eye coordination (e.g., hitting a target accurately)?",
        "How quickly can you react to visual or auditory cues?",
        "How steady and controlled are your hand movements during tasks requiring precision?"
    };

    string options[][5] = {
        {"Very slow", "Slow", "Average", "Fast", "Very fast"},
        {"Almost always", "Frequently", "Sometimes", "Rarely", "Never"},
        {"Very poor", "Poor", "Average", "Good", "Excellent"},
        {"Very slow", "Slow", "Average", "Fast", "Very fast"},
        {"Very unsteady", "Unsteady", "Average", "Steady", "Very steady"}
    };

    // Ask questions before the game
    cout << "Before the game:" << endl;
    for (int i = 0; i < 5; ++i) {
        beforeTotal += askQuestion(questions[i], options[i], 5);
    }

    // Ask questions after the game
    cout << "\nAfter the game:" << endl;
    for (int i = 0; i < 5; ++i) {
        afterTotal += askQuestion(questions[i], options[i], 5);
    }

    // Calculate percentage improvement
    double improvementPercentage = calculateImprovement(beforeTotal, afterTotal);

    // Display results
    cout << "\n--- Results ---" << endl;
    cout << "Total Score Before the Game: " << beforeTotal << " out of 25" << endl;
    cout << "Total Score After the Game: " << afterTotal << " out of 25" << endl;
    cout << "Improvement Percentage: " << improvementPercentage << "%" << endl;

    if (improvementPercentage > 0) {
        cout << "There has been an improvement in reaction time and motor skills." << endl;
    } else if (improvementPercentage < 0) {
        cout << "Reaction time and motor skills seem to have declined." << endl;
    } else {
        cout << "No change in reaction time and motor skills." << endl;
    }

    return 0;
}
