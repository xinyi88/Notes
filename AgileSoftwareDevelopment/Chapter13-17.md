# Command and active object
- physical and temporal decoupling
- active object: use of the command pattern
- suggested breaks the OO paradigm because it emphasizes functions over classes

# Chapter 14 Template method & strategy: inheritance vs delegation
- Template method
- Strategy pattern: solves the problem of inverting the dependencies of the generic alogrithm.

# Facade and mediator
- Common purpose: impose some kind of policy on another group of objects
- Facade: impose policy from above,  visible and constraining
- Mediator: impose policy from below, invisible and enabling

# Singleton and monostate
- Singleton benefit: cross platform, applicable to many class, can be created through derivation, lazy evalution
- Singleton costs: destructin is undefined, not inherited, efficiency, nontransparant
- Monostate: another way to implement singleton
- Monostate benefit: transparency, derivability, polymorphism, well-defined creation and destruction
- Monostate costs: no conversion, efficiency, presence, platform local

# null object
- always return valid objects, even when they fail
 
