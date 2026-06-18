# Help Desk Management System

A desktop-based Help Desk Management System developed using C# Windows Forms, ADO.NET, and SQL Server. The application helps organizations manage support tickets, assign agents, track ticket status, and maintain support records efficiently.

## Features

- Ticket Creation and Management
- Ticket Assignment
- Ticket Status Tracking
- Agent Management
- Search Functionality
- Filter Functionality
- Dashboard Reporting
- Chart-Based Analytics
- Data Validation
- CRUD Operations

## Technologies Used

- C#
- .NET 8
- Windows Forms (WinForms)
- ADO.NET
- SQL Server
- Visual Studio 2022

## Project Architecture

The project follows a layered architecture:

### App.Core
Contains:
- Models
- Services
- Business Logic
- Data Access Components

### App.UI
Contains:
- Windows Forms
- Dashboard
- Ticket Management Module
- Agent Management Module

## Database

The system uses SQL Server to store and manage data.

### Main Entities

#### Agent
- AgentID
- Name
- Email
- Department
- Phone

#### Ticket
- TicketID
- Title
- Description
- Priority
- Status
- Category
- Assigned Agent
- Created Date

## Functional Modules

### Ticket Management
- Add Ticket
- Update Ticket
- Delete Ticket
- View Ticket Details

### Agent Management
- Add Agent
- Update Agent
- Delete Agent
- View Agent Details

### Dashboard
- Ticket Statistics
- Status Overview
- Analytics and Reports

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/Abdullah-wazir-911/HelpDeskSystem.git
   ```

2. Open `HelpDeskSystem.sln` in Visual Studio 2022.

3. Restore dependencies and build the solution.

4. Run the application.

## Team Members

- Abdul Ghafar (Group Leader)  (F23BDOCS1M01201)
- Muhammad Abdullah            (F23BDOCS1M01198)
- Rehan Ali Arman              (F23BDOCS1M01220)

## Course Information

**Course:** Advanced Programming (COSC-5136)

**Department:** Computer Science

**University:** The Islamia University of Bahawalpur

## Purpose

This project was developed as a semester project for the Advanced Programming course to demonstrate practical implementation of desktop application development, database integration, and software architecture principles.

## License

This project is intended for educational purposes only.
