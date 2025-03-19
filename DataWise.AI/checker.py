import csv
from collections import Counter

def list_difficulty_counts(csv_filename):
    difficulty_counts = Counter()
    with open(csv_filename, "r", newline='', encoding="utf-8") as csvfile:
        reader = csv.DictReader(csvfile)
        for row in reader:
            difficulty_counts[row["Difficulty"]] += 1
    return difficulty_counts

csv_filename = "new_interview_questions.csv"
counts = list_difficulty_counts(csv_filename)
print("Difficulty counts:")
for difficulty, count in counts.items():
    print(f"{difficulty}: {count}")
