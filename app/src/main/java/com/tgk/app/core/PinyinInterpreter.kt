package com.tgk.app.core

class PinyinInterpreter(private val output: (String) -> Unit) {
    private val vars = mutableMapOf<String, String>()

    fun run(code: String) {
        code.lines().forEach { line ->
            val trimmed = line.trim()
            when {
                trimmed.startsWith("dayin") -> handlePrint(trimmed)
                trimmed.startsWith("bianliang") -> handleVar(trimmed)
                trimmed.startsWith("ruguo") -> output("条件暂不支持")
            }
        }
    }

    private fun handlePrint(line: String) {
        val regex = """dayin\s*\(\s*"(.*?)"\s*\)""".toRegex()
        val match = regex.find(line)
        match?.let {
            var content = it.groupValues[1]
            vars.forEach { (k, v) -> content = content.replace("$$k", v) }
            output(content)
        }
    }

    private fun handleVar(line: String) {
        val regex = """bianliang\s+(\w+)\s*=\s*(.*);""".toRegex()
        val match = regex.find(line)
        match?.let {
            vars[it.groupValues[1]] = it.groupValues[2].trim('"')
        }
    }
}
