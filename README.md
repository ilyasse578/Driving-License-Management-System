# DVLD - Driving & Vehicle License Management System 🚦

A comprehensive system for managing driver and vehicle licenses (Driving & Vehicle License Department - DVLD)

## Overview 📝
This system allows the issuance, renewal, and management of driving licenses. It streamlines the workflow of the licensing department, ensuring data integrity and preventing duplication. The system supports scheduling and managing various tests and handling user accounts.

## Key Services 🛠️
- 🆕 First-time license issuance
- 🔄 License re-examination
- ♻️ License renewal
- 🆔 Replacement for lost licenses
- ⚠️ Replacement for damaged licenses
- 🔓 Unlocking suspended licenses
- 🌍 International driving license issuance

## Applicant Information 👤
- National ID
- Full Name
- Date of Birth
- Address
- Phone Number
- Email
- Nationality
- Profile Photo

## License Classes 🚗🏍️
| Class | Description | Minimum Age | Validity | Fee |
|-------|------------|------------|---------|-----|
| 1 | 🏍️ Small Motorcycle | 18 | 5 years | $15 |
| 2 | 🏍️ Large Motorcycle | 21 | 5 years | $30 |
| 3 | 🚗 Car / Light Vehicle | 18 | 10 years | $20 |
| 4 | 🚕 Commercial Vehicle (Taxi/Limousine) | 21 | 10 years | $200 |
| 5 | 🚜 Agricultural Vehicle | 21 | 10 years | $50 |
| 6 | 🚌 Small/Medium Bus | 21 | 10 years | $250 |
| 7 | 🚚 Heavy Truck / Large Vehicle | 21 | 10 years | $300 |

## Tests & Examinations 🧪
- 👁️ **Medical Test**: Health & vision check. Fee: $10  
- 📖 **Theoretical Test**: Traffic laws & driving safety. Fee: $20  
- 🚦 **Practical Driving Test**: On-road evaluation. Fee varies by license class

## User Management 👨‍💻
- Add, edit, delete, and freeze users
- Assign roles and permissions
- Track user activities

## Request Management 📑
- Add, edit, filter, and view service requests
- Track payments and request status

## How to Run ⚙️
1. Clone or download the repository from GitHub.
2. Open the solution file `.sln` in Visual Studio 2022 or later.
3. Restore NuGet packages if required (`Guna.UI2.WinForms`).
4. Build the project and run.

## Screenshots 📷
*(Add screenshots here to show main forms and UI.)*

## Notes 📝
- Each applicant can hold multiple licenses from different classes.
- Requests are linked to applicants; duplicates are not allowed.
- All issued licenses and requests are tracked in the system.
