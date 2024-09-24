import matplotlib.pyplot as plt
import numpy as np
from watchdog.observers import Observer
from watchdog.events import FileSystemEventHandler
import threading
import time
import os
import queue

# Queue to communicate between threads
update_queue = queue.Queue()

# Function to read the maze and path from the file
def read_maze_file(file_path):
    with open(file_path, 'r') as file:
        lines = file.readlines()

    maze = []
    path = []

    for line in lines:
        line = line.strip()
        if line.startswith('('):  # This indicates the path coordinates
            coords = line.strip('()').split(', ')
            path.append((int(coords[0]), int(coords[1])))
        else:
            maze.append([int(char) for char in line])

    return np.array(maze), path

# Function to draw and update the maze and the path
def draw_maze(ax, maze, path, start, goal):
    ax.clear()  # Clear the previous plot
    ax.imshow(maze, cmap='binary', interpolation='nearest')
    
    # Highlight the path
    for (x, y) in path:
        ax.text(y, x, '•', color='red', fontsize=12, ha='center', va='center')

    # Mark the start and goal points
    ax.text(start[1], start[0], 'S', color='green', fontsize=12, ha='center', va='center')  # Start
    ax.text(goal[1], goal[0], 'G', color='blue', fontsize=12, ha='center', va='center')    # Goal

    plt.draw()  # Redraw the plot

# Event handler to notify when the file changes
class MazeFileHandler(FileSystemEventHandler):
    def __init__(self, file_path):
        self.file_path = file_path

    def on_modified(self, event):
        if event.src_path == self.file_path:
            print(f"File {self.file_path} has been modified, notifying the main thread...")
            update_queue.put('update')  # Send update signal to the main thread

# Function to start the observer that watches the file
def watch_file(file_path):
    event_handler = MazeFileHandler(file_path)
    
    observer = Observer()
    # Watch the directory containing the file
    observer.schedule(event_handler, os.path.dirname(file_path) or '.', recursive=False)
    
    observer.start()
    print(f"Started watching {file_path}")
    
    try:
        while True:
            time.sleep(1)  # Keep the observer running
    except KeyboardInterrupt:
        observer.stop()

    observer.join()

# Set the file path to the maze file
file_path = os.path.abspath('maze.txt')  # Use absolute path to avoid issues
print(f"Using file path: {file_path}")

# Read initial maze and path
maze, path = read_maze_file(file_path)
start_point = path[0]  # Start coordinate
goal_point = path[-1]  # Goal coordinate

# Create a plot for the maze
plt.ion()  # Turn on interactive mode in matplotlib
fig, ax = plt.subplots(figsize=(8, 8))
draw_maze(ax, maze, path, start_point, goal_point)

# Run the file watcher in a separate thread
watch_thread = threading.Thread(target=watch_file, args=(file_path,))
watch_thread.daemon = True  # Daemonize the thread so it doesn't block exit
watch_thread.start()

# Main loop to handle the GUI update
while True:
    try:
        # Wait for an update signal from the file watcher thread
        if not update_queue.empty():
            update_signal = update_queue.get()
            if update_signal == 'update':
                print("Reloading maze and updating plot...")
                maze, path = read_maze_file(file_path)
                start_point = path[0]
                goal_point = path[-1]
                draw_maze(ax, maze, path, start_point, goal_point)

        plt.pause(1)  # Keep the plot responsive
    except KeyboardInterrupt:
        break
