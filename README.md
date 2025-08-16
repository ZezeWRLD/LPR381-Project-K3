# 📊 Linear Programming & Integer Programming Solver

### 🔧 Subject: Linear Programming 381  
### 📑 Assessment: Project  
### 🧮 Total: 100 Marks  

---

## 📝 Project Overview

This project is a menu-driven `.NET` console application developed using Visual Studio. It solves **Linear Programming (LP)** and **Integer Programming (IP)** models and performs **Sensitivity Analysis**. It supports a wide range of algorithms and includes robust input/output handling.

---

## ✅ Features

- Accepts **any number** of decision variables and constraints.
- Solves:
  - Linear Programming models
  - Binary Integer Programming models (e.g., Knapsack)
  - Mixed Integer models
- Input from `.txt` file and output to `.txt` file.
- Menu-driven execution.
- Implements **Best Practices** with code comments and structure.
- Algorithms included:
  - Primal Simplex
  - Revised Primal Simplex
  - Branch & Bound Simplex
  - Cutting Plane
  - Branch & Bound Knapsack

---

## 📂 File Structure
```
LinearProgrammingSolver.sln
│
├── Solver.Core/                → Model (Algorithms & Domain Logic)
│   ├── Models/
│   │   ├── ProblemModel.cs          # Defines LP/IP problem (variables, constraints, RHS, etc.)
│   │   ├── Constraint.cs            # Constraint representation
│   │   ├── Variable.cs              # Decision variable representation
│   │   └── SolutionResult.cs        # Stores optimal solution + iterations
│   │
│   ├── Algorithms/
│   │   ├── PrimalSimplex.cs
│   │   ├── RevisedSimplex.cs
│   │   ├── BranchAndBound.cs
│   │   ├── CuttingPlane.cs
│   │   └── KnapsackSolver.cs
│   │
│   ├── Services/
│   │   ├── SolverService.cs         # Orchestrates which algorithm to use
│   │   └── SensitivityAnalysis.cs   # Handles all sensitivity analysis logic
│   │
│   └── Utils/
│       └── FileParser.cs            # Reads input file and outputs results
│
├── Solver.ConsoleApp/          → Console "Controller + View"
│   ├── Program.cs                   # Entry point (solve.exe)
│   ├── MenuController.cs            # Menu navigation logic
│   └── ConsoleView.cs               # Text-based output (tableaus, iterations, results)
│
├── Solver.BlazorUI/            → Blazor UI (View)
│   ├── Pages/
│   │   ├── Index.razor             # Home: upload file / select algorithm
│   │   ├── Solve.razor             # Displays canonical form, iterations, results
│   │   ├── Sensitivity.razor       # Sensitivity analysis options
│   │   └── ErrorHandling.razor     # Infeasible/unbounded cases
│   │
│   ├── Components/
│   │   ├── FileUpload.razor        # Input file uploader
│   │   ├── ResultTable.razor       # Table for displaying iterations
│   │   └── ChartView.razor         # Optional: visualize results with charts
│   │
│   └── wwwroot/                    # Static assets (CSS, icons, etc.)
│
└── README.md

```
  
## 📤 Output File Format

- Canonical Form of the model.
- All tableau/product form iterations.
- Optimal solution.
- Model status (Optimal, Infeasible, Unbounded).
- **All decimals rounded to three decimal places.**

---

## 🧪 Algorithms Implemented

| Algorithm                       | Features                                                                 |
|--------------------------------|--------------------------------------------------------------------------|
| Primal Simplex                 | Canonical form, full tableau iterations                                  |
| Revised Primal Simplex        | Product form & price-out method iterations                               |
| Branch & Bound Simplex        | Backtracking, sub-problem generation, iteration display, best candidate |
| Cutting Plane                 | Iteration and canonical form displays                                    |
| Knapsack Branch & Bound       | Full sub-problem exploration and optimal solution path                   |

---

## 🔬 Sensitivity Analysis Features

- Display/apply range changes for:
  - Basic and Non-Basic Variables
  - RHS values
  - Objective Coefficients
- Add new activities or constraints
- Display Shadow Prices
- Apply Duality and solve dual problem
- Determine Strong or Weak Duality

---

## ⚠️ Error Handling

- Infeasible model detection
- Unbounded model handling
- Algorithm compatibility validation

## 📺 Video Submission

- Submit a **video or OneDrive link in a PDF**.
- Video should demonstrate:
  - Input and output
  - Solving models
  - Sensitivity analysis
  - Error handling
- **Marks are based on the video**, not just the code.
  
## 🛠 Requirements

- .NET Console App (C# recommended)
- Visual Studio 2019/2022
- File I/O, Object-Oriented Programming
- Mathematical precision and validation
