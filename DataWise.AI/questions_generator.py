import openai
import csv
import time
import sys
import os

openai.api_key = "api_key"

DESIRED_MODEL = "gpt-4o-mini"

categories = {
    "AI (Data Science)": 2000,
    "General Software Engineering": 4000,
    "SQL": 1000,
    "Containers and Cloud": 1000,
    "DevOps": 1000
}

BATCH_SIZE = 500
CSV_FILENAME = "new_interview_questions.csv"

def get_available_model(desired_model):
    """
    Check available models from the OpenAI API and return the desired model
    if available; otherwise, fallback to "gpt-3.5-turbo".
    """
    try:
        models = openai.Model.list()
        available_models = [model["id"] for model in models["data"]]
        if desired_model in available_models:
            print(f"Using desired model: {desired_model}")
            return desired_model
        else:
            fallback = "gpt-3.5-turbo"
            print(f"Desired model '{desired_model}' not available. Falling back to '{fallback}'.")
            return fallback
    except Exception as e:
        print("Error retrieving available models:", e)
        sys.exit(1)

MODEL_TO_USE = get_available_model(DESIRED_MODEL)

def get_questions_for_category(category, num_questions):
    """
    Call the OpenAI API to generate a pipe-delimited formatted string containing
    num_questions interview questions for the given category.
    Expected output columns: ID | Category | Question | Answer | Difficulty (no header).
    """
    prompt = (
        f"You are a helpful assistant that generates interview questions. Please generate {num_questions} unique interview questions for the category \"{category}\". "
        "For each question, output a single record using the delimiter \" | \" (a pipe surrounded by spaces) to separate the fields. The fields must be in the following order: ID, Category, Question, Answer, Difficulty. "
        "Requirements:\n"
        "1. The Answer field must strictly begin with \"Answer: \" followed by the answer text.\n"
        "2. The Difficulty field must be exactly one of these values: Easy, Medium, Hard, Extremely Hard.\n"
        "3. Do not use commas as delimiters or include any extra commas that could be mistaken for field separators.\n"
        "4. Do not include a header row; output only the records in the specified format.\n"
        "5. Ensure that every question and answer is unique with no duplicates.\n"
        "Output the records exactly as specified."
    )

    try:
        response = openai.ChatCompletion.create(
            model=MODEL_TO_USE,
            messages=[
                {"role": "system", "content": "You are a helpful assistant."},
                {"role": "user", "content": prompt}
            ],
            temperature=0.7,
        )
    except Exception as e:
        print("API call failed:", e)
        sys.exit(1)

    text = response.choices[0].message.content.strip()
    print("Received output from API.")
    return text

def parse_csv_output(output_text):
    """
    Parse the pipe-delimited output text and return a list of rows.
    Each row is expected to have five columns.
    """
    lines = output_text.splitlines()
    rows = []
    for line in lines:
        parts = [part.strip() for part in line.split(" | ")]
        if len(parts) >= 5:
            rows.append(parts[:5])
    return rows

def save_progress(questions, csv_filename=CSV_FILENAME):
    """
    Save the current list of questions to a CSV file.
    Overwrites the file with the entire progress.
    """
    with open(csv_filename, mode="w", newline="", encoding="utf-8") as csvfile:
        fieldnames = ["ID", "Category", "Question", "Answer", "Difficulty"]
        writer = csv.DictWriter(csvfile, fieldnames=fieldnames)
        writer.writeheader()
        for question in questions:
            writer.writerow(question)
    print(f"Progress saved: {len(questions)} questions written to {csv_filename}")

def load_existing_progress(csv_filename=CSV_FILENAME):
    """
    Load existing questions from CSV and return them along with a dictionary mapping
    each category to the number of questions already generated.
    """
    questions = []
    progress = {cat: 0 for cat in categories}
    if os.path.exists(csv_filename):
        with open(csv_filename, mode="r", newline="", encoding="utf-8") as csvfile:
            reader = csv.DictReader(csvfile)
            for row in reader:
                questions.append(row)
                cat = row["Category"]
                if cat in progress:
                    progress[cat] += 1
    return questions, progress

def main():
    all_questions, progress = load_existing_progress()
    next_id = len(all_questions) + 1

    for category, total in categories.items():
        already = progress.get(category, 0)
        if already >= total:
            print(f"Category '{category}' is already complete with {already} questions.")
            continue

        num_batches = total // BATCH_SIZE
        completed_batches = already // BATCH_SIZE
        print(f"\nStarting category: {category} (Total: {total} questions in {num_batches} batches)")
        print(f"{completed_batches} batches completed for '{category}'. Resuming remaining batches...")

        for batch in range(completed_batches, num_batches):
            batch_questions = []
            attempts = 0
            max_attempts = 5
            print(f"\nStarting batch {batch+1} of {num_batches} for '{category}' (current questions in batch: {len(batch_questions)})")
            
            while len(batch_questions) < BATCH_SIZE and attempts < max_attempts:
                missing = BATCH_SIZE - len(batch_questions)
                print(f"Generating {missing} questions for '{category}' (current batch count: {len(batch_questions)}); attempt {attempts+1}")
                
                output = get_questions_for_category(category, missing)
                rows = parse_csv_output(output)
                
                if not rows:
                    attempts += 1
                    print("No valid data received from API call, retrying...")
                    time.sleep(1.3)
                    continue
                
                for row in rows:
                    if len(batch_questions) >= BATCH_SIZE:
                        break
                    batch_questions.append(row)
                attempts = 0
                time.sleep(2)
            
            if len(batch_questions) < BATCH_SIZE:
                print(f"Warning: Batch {batch+1} for '{category}' completed with only {len(batch_questions)} questions due to repeated failures.")
            
            for row in batch_questions:
                row[0] = str(next_id)
                next_id += 1
                row[1] = category
                question_dict = {
                    "ID": row[0],
                    "Category": row[1],
                    "Question": row[2],
                    "Answer": row[3],
                    "Difficulty": row[4]
                }
                all_questions.append(question_dict)
            
            print(f"Batch {batch+1} for '{category}' complete. Total questions so far for this category: {already + (batch - completed_batches + 1) * BATCH_SIZE}")
            
            save_progress(all_questions)

    print("\nAll batches complete.")

if __name__ == "__main__":
    main()
