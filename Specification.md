# Specification for Echolalia

## Introduction

Objective: Create the mobile application that will help users to learn words in foreign languages.

This specification outlines the development of a learning mobile app using Xamarin. The app's goal is to help users learn words conveniently on their mobile devices. 
By leveraging Xamarin's cross-platform capabilities, the app will be compatible with both Android and iOS devices, ensuring widespread accessibility.

The app will offer essential features such as user registration, authentication, and customizable user profiles. 
Users will be able to track their learning progress and watching their statistics. 
The application will also provide the opportunity to add your own words or take existing ones. 
Users will be able to practice skills through exercises and tests.

Development will follow the recommended Xamarin architecture, using C# as the programming language. 
Data will be stored in a suitable database, allowing easy retrieval and management of user information, words and statistics.

In summary, this specification aims to create a user-friendly words learning app that harnesses the power of Xamarin.

## Functional Requirements

### User Registration and Authentication

- Ability to create an account
- Ability to log in to the system using an email/login and password

### User Profile

- Ability to create and edit a user profile
- Ability to view user profile and statistics
- Ability to synchronize user data with the server database

### Learning Words

- Ability to add your own words or phrases (should automatically determine if it is a word or phrase)
- Ability to take existing words or phrases from the server database
- Ability to practice skills through exercises and tests

### Exercises

- Repeat words - choose the correct translation
- Write words - write the correct translation
- Insert words - insert the missing word to the phrase
- Write phrases - write the correct phrase
- Random - random exercise
- Should show the correct answer if the user made a mistake
- Ability to skip the word
- Should show the number of correct and incorrect answers at the end of the exercise

### Statistics

- Ability to view statistics of learning words per day in the form of a graph
(for example the number of words learned per day for last month)

### Settings

- Abbility to change the language of the application
- Abbility to change the number of words in the exercise
- Abbility to delete the account and all data

## Non-Functional Requirements

- Should be developed using Xamarin framework
- Pleasent and intuitive user interface
- Secure user information using authentication and data encryption

## Architecture

- Using the MVVM (Model-View-ViewModel) architecture for separating application logic and user interface.
- Using the server database for storing user information, words and statistics.
- Using local database for storing user words.
- Using the separate services for authorization, authentication, store user information, words and statistics.

## Technologies

- Development using Xamarin and the C# programming language.
- Using the SQLite database for storing.
- Using the ASP.NET Core Web API for the server side.
- Using the school server for hosting the database and the Web API.
- Using the Figma for design.

## Expected Database Structure

### Expected Entities

#### User

```json
{
  "id": "string",
  "email": "string",
  "password": "string",
  "name": "string",
  "surname": "string",
  "avatar": "string",
}
```

#### Word and UserWord

```json
{
  "id": "string",
  "word": "string",
  "translation": "string",(?)
  "type": "word | phrase",
}
```

#### Phrase

```json
{
  "id": "string",
  "phrase": "string",
  "translation": "string",
}
```

#### UserWord

```json
{
  "id": "string",
  "userId": "string",
  "word": "string",
  "translation": "string",
  "date": "string",
  "isLearned": "boolean", (?)
  "type": "word | phrase",
}
```

#### User Statistics

```json
{
  "id": "string",
  "userId": "string",
  "date": "string",
  "wordsLearned": "number",
  "wordsLearnedPerDay": [
    {
      "date": "string",
      "wordsLearned": "number",
    }
  ]
}
```

#### User Dictionary

```json
{
  "id": "string",
  "userId": "string",
  "words": [
    {
      "id": "string",
      "word": "string",
      "translation": "string",
      "type": "word | phrase",
    }
  ]
}
```

---
### Expected API

Registration and authentication
```
POST /api/auth/register
```
```
POST /api/auth/login
```

Get and update user information
```
GET /api/user/{id}
```
```
PUT /api/user/{id}
```

Get words by topic id from server database
```
GET /api/words/{id}
```

Add, update and delete words in user dictionary

```
GET /api/userdict/{id}
```
```
DELETE /api/userdict/{id}
```
```
POST /api/userdict/word/{id}
```
```
PUT /api/userdict/word/{id}
```
```
DELETE /api/userdict/word/{id}
```
