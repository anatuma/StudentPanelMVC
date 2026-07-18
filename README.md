# Student Panel

This is a small Blazor student panel. 
The app lets the user see students, open details, add a new student, assign courses and mark students as observed.
I used **Blazor WebAssembly** for the frontend and **ASP.NET Core API** for the backend. I chose WebAssembly because the UI runs as a separate client app and it shows API communication through HTTP.


## How to run

Run the API first:
```bash
dotnet run --project StudentPanel.Api
```
The API starts on: http://localhost:5028

Then run the client:
```bash
dotnet run --project StudentPanel.Client
```
The client starts on: http://localhost:5067


## Projects
- StudentPanel.Api - simple API with in-memory data (in DataStore.cs)
- StudentPanel.Client - Blazor WebAssembly application
- StudentPanel.Shared - shared DTO classes

## Main features
- Student list: StudentPanel.Client/Pages/StudentList.razor
- Student details: StudentPanel.Client/Pages/StudentDetails.razor
- Create student form: StudentPanel.Client/Pages/CreateStudent.razor
- Observed students page: StudentPanel.Client/Pages/ObservedStudents.razor
- 404 page: StudentPanel.Client/Pages/NotFound.razor
- Main layout and menu: StudentPanel.Client/Layout/MainLayout.razor and NavMenu.razor

## API communication
The typed client is in: StudentPanel.Client/Services/StudentsApiClient.cs

It uses HttpClient to call:
- GET /api/students
- GET /api/students/{id}
- POST /api/students
- GET /api/courses
- POST /api/students/{id}/courses

The API endpoints are in: StudentPanel.Api/Program.cs

The API uses in-memory data from: StudentPanel.Api/Data/DataStore.cs


## Blazor requirements

- OnInitializedAsync is used in StudentList.razor to load the student list when the page starts.
- OnParametersSetAsync is used in StudentDetails.razor to load data for the selected student by id from route.
- OnAfterRenderAsync is used in StudentDetails.razor for the clipboard JS interop.
- EditForm and validation are used in CreateStudent.razor and also in StudentDetails.razor for assigning a course.
- Data annotations are in StudentPanel.Shared/DTOs/StudentDto.cs.
- Shared observed students state is in StudentPanel.Client/Services/StateContainer.cs.
- The observed students counter is shown in StudentPanel.Client/Layout/NavMenu.razor.
- JS Interop is used in StudentDetails.razor to copy the student's email to the clipboard.
- RenderFragment<T> is used in StudentPanel.Client/Components/DataTable.razor.
- ErrorBoundary is used in StudentPanel.Client/Pages/StudentList.razor.


## Short questions

**How is OnInitializedAsync different from OnParametersSetAsync?**  
OnInitializedAsync runs when the component is created. OnParametersSetAsync runs when component parameters are set or changed, so it is good for loading data based on a route id.

**Why do we usually run DOM-dependent code in OnAfterRenderAsync?**  
Because the HTML must already be rendered before JavaScript can safely work.

**Why should you be careful with state registered as Singleton in Blazor Server?**  
Because Singleton state can be shared between users. For user-specific data it can cause wrong data to be visible to another user.

**What does a typed client give you compared to calling HttpClient directly in every component?**  
It keeps API calls in one place, so components are cleaner and the API code is easier to change.

**How is NavLink different from a regular `<a>` link?**  
NavLink knows when its route is active and can add active styling. It is better for menus in Blazor.

**What is RenderFragment<T> used for?**  
It is used to pass a template for rendering data. In this project it is used in DataTable.razor to render table rows for different item types.

**When does JS Interop make sense, and when is it better to stay with Blazor?**  
JS Interop makes sense for browser features that Blazor does not handle directly, like clipboard access. For normal UI logic and rendering it is better to use Blazor.

**What problem does ErrorBoundary solve, and what should it not replace?**  
ErrorBoundary prevents one component error from breaking the whole page. It should not replace normal validation or normal API error messages.