#!/bin/bash

# Define the target directory (use current directory by default)
TARGET_DIR="${1:-.}"

# Check if the directory exists
if [ ! -d "$TARGET_DIR" ]; then
    echo "Error: Directory $TARGET_DIR does not exist."
    exit 1
fi

# Loop through files in the directory
for FILE in "$TARGET_DIR"/*; do
    # Check if it is a file
    if [ -f "$FILE" ]; then
        # Extract the base name of the file
        BASENAME=$(basename "$FILE")
        DIRNAME=$(dirname "$FILE")

        # Replace 'BlondeHair' with 'BlackRobe' in the filename
        NEW_BASENAME=$(echo "$BASENAME" | sed 's/BlondeHair/BlackRobe/g')
        NEW_NAME="$DIRNAME/$NEW_BASENAME"

        # Rename the file if the name has changed
        if [ "$FILE" != "$NEW_NAME" ]; then
            mv "$FILE" "$NEW_NAME"
            echo "Renamed: $FILE -> $NEW_NAME"
        fi
    fi

done

