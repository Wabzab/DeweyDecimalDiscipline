# DeweyDecimalDiscipline
The Dewey Decimal System is fundamental to how everything is organised in the library. But the
head librarian has found that even novice librarians sometimes get bored when they must learn
about the details of the system. The purpose of the training software that you will develop for the
library, is to make learning about the system fun and engaging.
After using the training software, the user must be able to:
- Correctly replace a book in its position on a shelf.
- Identify which broad area a book belongs to.
- Find the call number for a specific topic.

Note: The application makes use of a local database to keep track of scores and achievements which is stored in the user's `Documents` folder. 

## Requirements
- Visual Studio 2019 (or newer)
- .NET 6.0
- Microsoft.EntityFrameworkCore.SQLite
- Microsoft.EntityFrameworkCore.Tools

## Building
- Open `DeweyDecimalDiscipline.sln` in Visual Studio.
- Select `Build -> Build Solution` in the top bar.

## How To Use
Launching the app will land you on the home page, where you can view your statistics
and achievements for each available task, as well as access the tasks themselves.

The tasks available include:
- Replacing Books - Sort books by call number in ascending order.
- Identifying Areas - Match top level call number names to their description, and vice versa.
- Finding Call Numbers - Given a specific call number, the user must navigate a tree of options that get more specific per level until they find the correct call number.
