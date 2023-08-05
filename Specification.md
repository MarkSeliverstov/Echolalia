# Specification for Echolalia

## Introduction

Objective: Create the mobile application that will help users to learn words in foreign languages.

This specification outlines the development of a learning mobile app using Xamarin. The app's goal is to help users learn words conveniently on their mobile devices. 

Users will be able to track their learning progress and watching their statistics. 
The application will also provide the opportunity to add your own words or take existing ones from the server database.
Users will be able to practice skills through exercises and tests.
Development will follow the recommended Xamarin architecture, using C# as the programming language. 
Data will be stored in a suitable database, allowing easy retrieval and management of user information, words and statistics.

In summary, this specification aims to create a user-friendly words learning app that harnesses the power of Xamarin.

## The main goals

- Create a mobile application for multiple platforms (Android, iOS)
- Create backend for app (exersises, tests, words, statistics, settings)
- Create frontend and main functionality in the Xamarin

Using an external server will allow users to synchronize their data between devices and not lose it when reinstalling the application.
Server will be hosted on the school server.

## Functional Requirements

### User Profile

- Ability to edit a user profile (Name)
- Ability to view user profile and statistics

### Learning Words

- Ability to add your own words
- Ability to take existing words or phrases from the server database
- Ability to practice skills through exercises and tests
- Ability to mark words as learned or favorite

### Exercises

- Repeating words - translation from the foreign language to the native language
- Learning words - translation from the foreign language to the native language (choice)
- Training words - translation from the native language to the foreign language (writing)

### Requirements for exercises
- Should show the correct answer if the user made a mistake
- Ability to skip the word
- Should show the number of correct and incorrect answers at the end of the exercise

### Statistics

- Ability to view statistics of learning words per day in the form of a graph
(for example the number of words learned per day for last month)

### Settings

- Abbility to change the language of the application
- Abbility to change the number of words in the exercise
- Abbility to delete all data

## Non-Functional Requirements

- Should be developed using Xamarin framework
- Pleasent and intuitive user interface
## Architecture

- Using the MVVM (Model-View-ViewModel) architecture for separating application logic and user interface.
- Using local database for storing user words.
- The application must be scalable

## Technologies

- Development using Xamarin and the C# programming language.
- Using the SQLite database for storing.
- Using the Figma for design.

## Expected Database Structure

**Word**

```json
{
  "ID": "string",
  "Original": "string",
  "Translation": "string",
  "isAddedbyUser": "boolean",
  "Progress": "Learning progress"
  "lastRepetition": "string",
  "LastPracticed": "DateTime",
  "IsFavorite": "boolean",
}
```