import os
from datetime import datetime

solutions_directory = 'AdventOfCode/Solutions'
template_path = 'Solution.cs.template'

if __name__ == "__main__":
    years = list(range(2015, datetime.now().year + 1))
    days = [f"{i:02}" for i in range(1, 26)]

    template_contents = open(template_path, 'r').read()

    for year in years:
        year_path = os.path.join(solutions_directory, F"Year{year}")

        if not os.path.exists(year_path):
            os.makedirs(year_path)

        for day in days:
            day_path = os.path.join(year_path, f"Day{day}")

            if not os.path.exists(day_path):
                os.makedirs(day_path)

            file_path = os.path.join(day_path, f"Solution.cs")

            if not os.path.exists(file_path):
                populated_template_contents = template_contents.replace("<Year>", str(year)).replace("<Day>", str(day))

                with open(file_path, 'w') as file:
                    file.write(populated_template_contents)
