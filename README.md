# AAL_Project_Course
This git hub was created for the project course assisted living. The goal therefore is to develope a application with the EduExo prothesis.

## Team-Members & (Tasks)
- Martina Adler
- Sophia Pellegrini
- Julia Jackermaier
- Sandro Müller (Backend-Software Arduino)

Tasks not distributed: 
- (Frontend-Software Unity)
- (Interface between Arduino and Unity)
- (Frontend-Software Unity)
  
## Concept of the project 
The concept for Post-Elbow Surgery Rehabilitation Device by Jan Veneman 

Introduction: This concept focuses on utilizing a specialized device for post-surgery rehabilitation of the elbow joint, which aims to facilitate the healing process and minimize re-injury risk. 

Initial Patient Situation: The patient has undergone elbow joint surgery, necessitating a careful and controlled rehabilitation process. 

Rehabilitation Objective: In the rehabilitation phase, the patient must restrict the degree of elbow flexion to allow sufficient time for proper healing. 

Our Innovative Approach: To achieve this objective effectively, we propose using a custom device to monitor and control the degree of elbow flexion. Our solution involves creating a Unity application where objects must be manipulated from one platform to another. Through this virtual interface, we can precisely regulate the elbow flexion angle, allowing patients to learn and maintain the appropriate range of motion. 

Rehabilitation Session Workflow: 
- In the physical world, force is applied through a pinching motion when an object is grasped. 
- This applied force is detected by an Electromyography (EMG) sensor, initiating the Unity application. 
- Within the Unity application, an object materializes in the user's virtual hand. 
- A stepper motor provides guidance, directing the arm to a specific angle within the elbow joint. 
- The actual joint angle is continuously monitored through a motor encoder. 
- If the patient exceeds the target angle, the device offers resistance to deter further movement. 
- Multiple training sessions are conducted to familiarize patients with their current rehabilitation status. 

Conclusion: Our approach allows precise control of the elbow's flexion angle within the Unity platform, enabling optimal training and rehabilitation tailored to the patient's needs. 

Benefits and Considerations: 
- Potential benefits include enhanced patient rehabilitation and reduced risk of joint re-injury. 
- The Unity platform's flexibility permits accurate angle adjustments for personalized treatment. 

## Structure 
The project consist of two main software components the frontend (Unity) and the backend (Arduino-Software) and utilising this combination to enhance the rehabilitation of patients after a post elbow surgery. This project should showcase the ability of the EduExo or similar Exo-Skeletons. 
 
