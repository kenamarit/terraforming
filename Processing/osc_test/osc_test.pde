import SimpleOpenNI.*;
import oscP5.*;
import netP5.*;
 
OscP5 oscP5;
NetAddress myRemoteLocation; 
SimpleOpenNI  context;
 
void setup()
{
  // start oscP5, telling it to listen for incoming messages at port 5001 */
  oscP5 = new OscP5(this,7400);
 
  // set the remote location to be the localhost on port 5001
  myRemoteLocation = new NetAddress("127.0.0.1",7400);
  
  
  // instantiate a new context
  context = new SimpleOpenNI(this);
 
  // enable depthMap generation 
  context.enableDepth();
 
  // enable skeleton generation for all joints
  context.enableUser(SimpleOpenNI.SKEL_PROFILE_ALL);
 
  background(200,0,0);
  stroke(0,0,255);
  strokeWeight(3);
  smooth();
 
  // create a window the size of the depth information
  size(context.depthWidth(), context.depthHeight()); 
}
 
void draw()
{
  // update the camera
  context.update();
 
  // draw depth image
  image(context.depthImage(),0,0); 
 
  // for all users from 1 to 10
  int i;
  for (i=1; i<=10; i++)
  {
    // check if the skeleton is being tracked
    if(context.isTrackingSkeleton(i))
    {
      // draw the skeleton
      drawSkeleton(i);  
      
      sendJointPosition(i);
 
      // draw a circle for a head 
      circleForAHead(i);
    }
  }
}
 
// draws a circle at the position of the head
void circleForAHead(int userId)
{
  // get 3D position of a joint
  PVector jointPos = new PVector();
  context.getJointPositionSkeleton(userId,SimpleOpenNI.SKEL_HEAD,jointPos);
  // println(jointPos.x);
  // println(jointPos.y);
  // println(jointPos.z);
 
  // convert real world point to projective space
  PVector jointPos_Proj = new PVector(); 
  context.convertRealWorldToProjective(jointPos,jointPos_Proj);
 
  // a 200 pixel diameter head
  float headsize = 200;
 
  // create a distance scalar related to the depth (z dimension)
  float distanceScalar = (525/jointPos_Proj.z);
 
  // set the fill colour to make the circle green
  fill(0,255,0); 
 
  // draw the circle at the position of the head with the head size scaled by the distance scalar
  ellipse(jointPos_Proj.x,jointPos_Proj.y, distanceScalar*headsize,distanceScalar*headsize);  
}
 
// draw the skeleton with the selected joints
void drawSkeleton(int userId)
{  
  // draw limbs  
  context.drawLimb(userId, SimpleOpenNI.SKEL_HEAD, SimpleOpenNI.SKEL_NECK);
 
  context.drawLimb(userId, SimpleOpenNI.SKEL_NECK, SimpleOpenNI.SKEL_LEFT_SHOULDER);
  context.drawLimb(userId, SimpleOpenNI.SKEL_LEFT_SHOULDER, SimpleOpenNI.SKEL_LEFT_ELBOW);
  context.drawLimb(userId, SimpleOpenNI.SKEL_LEFT_ELBOW, SimpleOpenNI.SKEL_LEFT_HAND);
 
  context.drawLimb(userId, SimpleOpenNI.SKEL_NECK, SimpleOpenNI.SKEL_RIGHT_SHOULDER);
  context.drawLimb(userId, SimpleOpenNI.SKEL_RIGHT_SHOULDER, SimpleOpenNI.SKEL_RIGHT_ELBOW);
  context.drawLimb(userId, SimpleOpenNI.SKEL_RIGHT_ELBOW, SimpleOpenNI.SKEL_RIGHT_HAND);
 
  context.drawLimb(userId, SimpleOpenNI.SKEL_LEFT_SHOULDER, SimpleOpenNI.SKEL_TORSO);
  context.drawLimb(userId, SimpleOpenNI.SKEL_RIGHT_SHOULDER, SimpleOpenNI.SKEL_TORSO);
 
  context.drawLimb(userId, SimpleOpenNI.SKEL_TORSO, SimpleOpenNI.SKEL_LEFT_HIP);
  context.drawLimb(userId, SimpleOpenNI.SKEL_LEFT_HIP, SimpleOpenNI.SKEL_LEFT_KNEE);
  context.drawLimb(userId, SimpleOpenNI.SKEL_LEFT_KNEE, SimpleOpenNI.SKEL_LEFT_FOOT);
 
  context.drawLimb(userId, SimpleOpenNI.SKEL_TORSO, SimpleOpenNI.SKEL_RIGHT_HIP);
  context.drawLimb(userId, SimpleOpenNI.SKEL_RIGHT_HIP, SimpleOpenNI.SKEL_RIGHT_KNEE);
  context.drawLimb(userId, SimpleOpenNI.SKEL_RIGHT_KNEE, SimpleOpenNI.SKEL_RIGHT_FOOT);  
}
 
// Event-based Methods
 
// when a person ('user') enters the field of view
void onNewUser(int userId)
{
  println("New User Detected - userId: " + userId);
 
 // start pose detection
  context.startPoseDetection("Psi",userId);
}
 
// when a person ('user') leaves the field of view 
void onLostUser(int userId)
{
  println("User Lost - userId: " + userId);
}
 
// when a user begins a pose
void onStartPose(String pose,int userId)
{
  println("Start of Pose Detected  - userId: " + userId + ", pose: " + pose);
 
  // stop pose detection
  context.stopPoseDetection(userId); 
 
  // start attempting to calibrate the skeleton
  context.requestCalibrationSkeleton(userId, true); 
}
 
// when calibration begins
void onStartCalibration(int userId)
{
  println("Beginning Calibration - userId: " + userId);
}
 
// when calibaration ends - successfully or unsucessfully 
void onEndCalibration(int userId, boolean successfull)
{
  println("Calibration of userId: " + userId + ", successfull: " + successfull);
 
  if (successfull) 
  { 
    println("  User calibrated !!!");
 
    // begin skeleton tracking
    context.startTrackingSkeleton(userId); 
  } 
  else 
  { 
    println("  Failed to calibrate user !!!");
 
    // Start pose detection
    context.startPoseDetection("Psi",userId);
  }
}

void sendJointPosition(int userId)
{
  PVector jointPos = new PVector();   // create a PVector to hold joint positions
  
  context.getJointPositionSkeleton(userId,SimpleOpenNI.SKEL_HEAD,jointPos);
  
  OscMessage headMessage = new OscMessage("/head");
  
  headMessage.add(jointPos.x);
  headMessage.add(jointPos.y);
  headMessage.add(jointPos.z);
 
  /*
  // get the joint position of the left hand
  context.getJointPositionSkeleton(userId,SimpleOpenNI.SKEL_LEFT_HAND,jointPos);
 
   // create an osc message
  OscMessage leftarmMessage = new OscMessage("/leftarm");
 
 // send joint position of y axis by OSC
  leftarmMessage.add(jointPos.x);
  leftarmMessage.add(jointPos.y); 
  leftarmMessage.add(jointPos.z);
 
 
  // get the joint position of the right hand
  context.getJointPositionSkeleton(userId,SimpleOpenNI.SKEL_RIGHT_HAND,jointPos);
 
   // create an osc message
  OscMessage rightarmMessage = new OscMessage("/rightarm");
 
 // send joint position of y axis by OSC
  rightarmMessage.add(jointPos.x);
  rightarmMessage.add(jointPos.y); 
  rightarmMessage.add(jointPos.z);
  
  */
 
  // send the messages
  //oscP5.send(rightarmMessage, myRemoteLocation);  
  //oscP5.send(leftarmMessage, myRemoteLocation); 
 
 oscP5.send(headMessage, myRemoteLocation);  
}

