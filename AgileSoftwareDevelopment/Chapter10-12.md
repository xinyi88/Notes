# LIP
- Subtypes must be substitutable for their base types
o1 typeof S
o2 typeof T
P all programs -> T
If o1 is substituted for o2, then S subtype of T 
- To be extensible without modification
# DIP
- Dependency-inversion principle
- High-level and low-level modules, both should depend on abstractions
- Details should depend on abstractions
- To make the abstractions and details isolate from each other, so the code is much easier to maintain.
# ISP
- Interface-segregation principle
- Avoid "fat" interfaces.
- By breaking "fat" interfaces to many client-specify interfaces, which is only for particular client.