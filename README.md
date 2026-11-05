The NeoLanguage Project:
    This project is an ecosystem project or you can say a compiler project. It's not monolithic. It's modular. It has modules and the modules are shared libraries:

    neofe(lang) -> It's a frontend module of any language. It takes the text, turns it to AST, and the AST to the NeoIR. The IR of this project. You can code neofeyourlang.so, you won't have to get worried, because NeoIR is going to have stable and nightly versions and strict ABI in stable versions.

    neome(platform) -> It's a middle end module. It takes the NeoIR, turns it to the backend's IR. Like you can code neomellvm and it turns NeoIR to LLVM IR. I'm going to code neomecs module and turn NeoIR to C# code. I plan to code neomellvm later.

    neolex(lang) -> It's a different module. It's an FFI (Foreign Function Interface) lexer. I'm going to code neolexc to lex C headers without functions with bodies to get the ecosystem. If I don't make this move, the NeoLanguage Project may die.