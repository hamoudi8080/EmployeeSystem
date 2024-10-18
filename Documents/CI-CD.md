# My Experience with Deploying the Project and Understanding CI/CD

After successfully creating a Docker image for my project, I pushed it to Azure Container Registry, ready to deploy the backend which would depend on this image. The first deployment went smoothly; everything was working as expected!

However, things took a turn when I made some changes to the code. After updating the code, I rebuilt the Docker image locally and pushed it again. To my surprise, I realized I had to push the updated image to the container registry each time I made changes. This repetitive process of building the image and pushing it felt exhausting and time-consuming.

At that moment, I wondered: **Do I really have to go through this every time I make a code change?** It became clear that this manual process could quickly become a bottleneck in my workflow.

That’s when I discovered the concept of **Continuous Integration and Continuous Deployment (CI/CD)**. CI/CD is a set of practices that automates the process of integrating code changes and deploying applications. With CI/CD, I could automate the building and pushing of Docker images whenever I make changes to my code. This would save me a lot of time and effort, allowing me to focus more on developing features rather than managing deployments.

By implementing CI/CD, I can now ensure that every change I make is automatically tested and deployed, making my development process much more efficient and streamlined. This experience not only enhanced my understanding of the deployment pipeline but also highlighted the importance of automation in modern software development.



