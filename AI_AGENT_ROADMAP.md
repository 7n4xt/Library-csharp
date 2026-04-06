# Library-csharp Agent Roadmap (Slow and Credible)

This file is the single source of truth for any AI agent working on this project.

Goal:
- Build the complete WinForms + MySQL library app from scratch.
- Keep each push small and believable (target 80-180 changed lines per push, hard max 200).
- Keep UI close to native Microsoft Windows look and behavior.

## 1) Non-Negotiable Delivery Rules

- One small feature per push.
- Do not mix unrelated files in the same push.
- Keep naming, formatting, and comments consistent.
- Avoid giant auto-generated dumps in one commit.
- Before each push: build/check compile and run a quick manual test.

Push size policy:
- Preferred: 80-180 changed lines.
- Allowed: 30-200 changed lines.
- If a task is bigger, split into two pushes.

Commit message style:
- feat(db): add connection factory and config reader
- feat(models): add Book and Borrowing entities
- feat(ui): load books into DataGridView on startup
- fix(repo): handle null genre in search mapping

## 2) Styling Direction (Windows / Microsoft-like)

UI must feel native Windows, not custom web-like:
- Font: Segoe UI, 9F or 10F.
- Use standard WinForms spacing (8, 12, 16 px rhythm).
- Use OS colors/SystemColors where possible.
- Controls should be classic and readable (no flashy theme packs).
- Buttons and interactions should be simple and predictable.
- Prefer TableLayoutPanel/Anchor/Dock for stable resize behavior.
- DataGridView style should stay clean, dense, and professional.

Suggested defaults to apply gradually:
- Form StartPosition = CenterScreen
- Form MinimumSize set
- DataGridView ReadOnly = true for listing grid
- DataGridView SelectionMode = FullRowSelect
- DataGridView MultiSelect = false
- DataGridView AutoSizeColumnsMode = Fill (or DisplayedCells for key columns)

## 3) Project Execution Order

Current status:
- Empty code skeleton.
- migration.sql created first.

Execution phases:
1. Database script and config wiring
2. Domain models
3. DB connection utility
4. Book repository CRUD
5. Main form listing + search
6. Add/Edit book form
7. Edit flow from grid double-click
8. Delete flow with confirmation
9. Validation and error handling pass
10. Bonus borrowings and cover uploads

## 4) Micro-Push Plan (Follow in Order)

### Push 01 (done now)
- Add migration.sql with Books and Borrowings tables.

### Push 02 (done)
Files:
- App.config
- LibraryManagement.csproj
Goal:
- Add MySQL connection string placeholder.
- Add package reference to MySql.Data.
Line budget:
- 40-120 lines.

### Push 03 (done)
Files:
- Models/Book.cs
- Models/Borrowing.cs
Goal:
- Add clean model classes with properties matching DB columns.
Line budget:
- 40-120 lines.

### Push 04 (done)
Files:
- Database/DbConnection.cs
Goal:
- Implement method returning opened MySqlConnection using App.config key.
Line budget:
- 40-100 lines.

### Push 05 (done)
Files:
- Database/BookRepository.cs
Goal:
- Implement GetAll + internal mapper only.
Line budget:
- 80-180 lines.

### Push 06 (done)
Files:
- Database/BookRepository.cs
Goal:
- Implement Search(keyword).
Line budget:
- 40-120 lines.

### Push 07 (done)
Files:
- Database/BookRepository.cs
Goal:
- Implement Add(Book).
Line budget:
- 40-120 lines.

### Push 08 (done)
Files:
- Database/BookRepository.cs
Goal:
- Implement Update(Book) + Delete(id).
Line budget:
- 60-160 lines.

### Push 09
Files:
- Program.cs
- Views/MainForm.cs
- Views/MainForm.Designer.cs
Goal:
- Show grid and load GetAll() on startup.
Line budget:
- 100-200 lines.

### Push 10
Files:
- Views/MainForm.cs
- Views/MainForm.Designer.cs
Goal:
- Add search box + button and bind Search().
Line budget:
- 60-160 lines.

### Push 11
Files:
- Views/AddEditBookForm.cs
- Views/AddEditBookForm.Designer.cs
Goal:
- Build Add form layout with validations.
Line budget:
- 100-200 lines.

### Push 12
Files:
- Views/MainForm.cs
- Views/AddEditBookForm.cs
Goal:
- Add open-add flow and refresh grid after insert.
Line budget:
- 60-160 lines.

### Push 13
Files:
- Views/MainForm.cs
- Views/AddEditBookForm.cs
Goal:
- Add edit flow (double-click row).
Line budget:
- 80-180 lines.

### Push 14
Files:
- Views/MainForm.cs
Goal:
- Add delete button + confirmation MessageBox + refresh.
Line budget:
- 40-120 lines.

### Push 15
Files:
- Services/LibraryService.cs (optional)
- Main form and Add/Edit form touch-ups
Goal:
- Validation hardening and user-friendly error messages.
Line budget:
- 60-180 lines.

### Push 16 (bonus)
Files:
- Models/Borrowing.cs
- Database (new Borrowing repository or methods)
- Views updates
Goal:
- Borrowing lifecycle and availability update.
Line budget:
- 80-200 lines.

### Push 17 (bonus)
Files:
- Views/AddEditBookForm.cs
- Views/AddEditBookForm.Designer.cs
- Resources/Covers (folder usage)
Goal:
- Cover upload, preview, and path persistence.
Line budget:
- 100-200 lines.

## 5) Definition of Done

Minimum done:
- CRUD books works end-to-end.
- Search works on Titre/Auteur/Genre/ISBN.
- Main list updates after add/edit/delete.
- Empty/invalid field validation exists.
- App compiles without errors.

Nice-to-have done:
- Borrowings feature complete.
- Cover upload/display works.

## 6) Agent Prompt Template (for each next session)

Use this prompt:

"Continue Library-csharp from AI_AGENT_ROADMAP.md at the next unfinished push only. Keep diff below 200 lines, keep Windows-native WinForms styling, compile-ready code, and do not touch unrelated files."

## 7) Manual Test Checklist Per Push

- Build succeeds.
- App opens.
- Feature of current push works.
- No regression in previous feature.
- Changed lines still in allowed range.
