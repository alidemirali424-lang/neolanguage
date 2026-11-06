This is a programming language. I'm going to code its frontend. It's T-Lang.

1- It's a flow based language. Main operator is >>, like a pipeline:

x operation >> y operation >> z operation

2- It doesn't only have types, it also has types of types. They usually identify the type's purpose.

2.1- data: This type of type is like a CPU register. You can put a value as the length of the type in it. Max length is 64 bits (i32 is 32 bits integer number):

data i32 5 >> x operation

! The template is: (type of type) (type) (value)

3- In T-Lang, when you want to basic ALU operations with 'data' type of type, you don't do this:

data i32 5 + 9

If you want to do something like this, first, you should define the thing:

data i32 5 // example

Then, pipeline and operation:

... >> x operation

In ALU operations, you can use +, -, / and *. For example, if you want to create value in this virtual T-Lang register, and then do a basic ALU operation, you should do +value, -value, /value or *value:

data i32 5 >> +9 >> -5 >> /3 >> *6 // output: 18