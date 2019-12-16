# ToyRobotSimulator

ToyRobotSimulator is a small WPF application using the MVVM pattern which simulates a Robot moving on a 5x5 grid plane.
The grid is a 5x5 blue square and the Robot represents a yellow grid cell. When the user selects Move method, the Robot's grid cell is moved by one grid cell.
The project uses UnityContainer as its IoC container and MSTest for unit testing.

# Installation

Copy all projects from the solution inside a folder.
To run the application, simply select the View Project as the Startup project and press Start.

# Usage

Once the application has started, the first step is to click on the 'Place' button to place the Robot on the grid.
The Robot is placed based on the values inside the place.properties file which can be found inside the View/Files/ folder.
Once the Robot has been placed, four new Buttons will become active: Move, Left, Right and Report.
-- Move button moves the Robot with one grid cell in the direction it currently has.
-- Left and Right rotate change the directions of the Robot.
-- Report displays a small output with the Robot's current position and direction.

If the Robot is placed outside its grid, an error message will pop up. The same happens if the Robot reaches a border and cannot move in that direction anymore.
