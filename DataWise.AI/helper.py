import csv

valid_difficulties = {"Easy", "Medium", "Hard", "Extremely Hard"}
updated_rows = []

with open("new_interview_questions.csv", "r", newline='', encoding="utf-8") as infile:
    reader = csv.DictReader(infile)
    fieldnames = reader.fieldnames
    for row in reader:
        if row["Difficulty"] not in valid_difficulties:
            row["Difficulty"] = "Medium"
        updated_rows.append(row)

with open("interview_questions.csv", "w", newline='', encoding="utf-8") as outfile:
    writer = csv.DictWriter(outfile, fieldnames=fieldnames)
    writer.writeheader()
    writer.writerows(updated_rows)
