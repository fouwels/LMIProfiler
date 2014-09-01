Project from the Rewired State LMI hackathon 2014. Was put together in around 6 hours, the code is horrible.

Uses the ONET data to generate the ideal career for a user.
We get a random selection of the ONET job tites and present them to the user, and the user selects whether it interests or disinterests them. They go through list one by one, as each job has a set of values for the ONET categories, we can use the accepted/rejected sets to generate a "average" and ideal set of values. We can then match this ideal profile to the nearest fitting job catergory and suggest this to the user, along with a breakdown of the final scores.

The stats generation aspect was built as a seperate module [here](https://github.com/hrickards/lmi_for_all) in order to offload the processing on an external server, rather than run it on the phone itself.

##Requirements
-Json.net

Uses the Windows "Universal App" platform to support both Windows 8 and Windows Phone 8+

Build with Visual Studio 2013, requires at minimum Update 3 to support Universal Apps.
