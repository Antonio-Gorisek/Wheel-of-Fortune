# Fake Wheel of Fortune

This project is an implementation of a **fake dynamic** Wheel of Fortune system that uses a "pie" element to simulate the spinning wheel. Even though you define the chances for each outcome (e.g., sword 58, bow 77, potion 28), everything will ultimately be **balanced** so that the total sum of all possible outcomes equals 100. This system ensures fairness while still providing the appearance of randomness.

![Screenshot_2](https://github.com/user-attachments/assets/5f72b982-5e4b-4a45-965b-26a03707e23a)


## Why use this system?

This system is ideal for games that require server-side logic or games that want to implement balance based on player statistics while still maintaining a sense of unpredictability and excitement.

### Server-side logic application

If you're developing a game with server-side logic, this system allows the server to pre-decide where the wheel will stop (e.g., based on player data or other conditions). While the server already makes the decision, the wheel is shown to the player with smooth, random animations, creating the illusion that the outcome is random. This approach is useful for games that need to store results in a database and return information to the player.

![Screenshot_4](https://github.com/user-attachments/assets/20f3be12-eae9-414a-a0bb-59d750faf39e)


### Balancing the game based on statistics

If you're developing a game that uses player statistics (e.g., level, playtime, karma, etc.), this system allows you to dynamically adjust the rewards based on those factors. For example, you can set conditions so that lower-level players cannot randomly win the best rewards, ensuring fair progression and balance within the game.

### Key Features

- **Smooth animations**: The wheel spins with smooth, random stopping points within the chosen segment.
- **Flexibility**: This system can easily be adapted for different games and applications that use server-side logic.
- **Statistical balancing**: The system can be used to adjust rewards based on player statistics, increasing fairness and balance in the game.



## Important Notes

This project does **not** include any pop-ups, prefabs, sounds, or similar features. The goal was to create a system without dependencies, making it easy to integrate into your projects without the need to remove or modify dozens of things.
It contains a simple UI element, but this UI element is completely independent and can be easily modified or removed.
