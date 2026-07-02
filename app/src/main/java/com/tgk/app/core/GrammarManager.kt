package com.tgk.app.core

import android.content.Context
import java.io.*

object GrammarManager {
    val grammarMap = mutableMapOf<String, String>()

    fun init(context: Context) {
        try {
            val input = context.assets.open("GrammarData/default_grammar.txt")
            BufferedReader(InputStreamReader(input)).use { reader ->
                reader.forEachLine { line ->
                    val parts = line.split("|")
                    if (parts.size == 2) grammarMap[parts[0].trim()] = parts[1].trim()
                }
            }
        } catch (e: Exception) {
            e.printStackTrace()
        }
    }

    fun getRawText(): String {
        return grammarMap.entries.joinToString("\n") { "${it.key}|${it.value}" }
    }

    fun exportToFile(path: String) {
        File(path).writeText(getRawText())
    }

    fun importFromFile(path: String) {
        File(path).forEachLine { line ->
            val parts = line.split("|")
            if (parts.size == 2) grammarMap[parts[0].trim()] = parts[1].trim()
        }
    }
}
