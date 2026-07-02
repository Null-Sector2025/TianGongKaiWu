package com.tgk.app.core

import android.content.Context
import java.io.File

object ProjectManager {
    private lateinit var projectDir: File

    fun init(context: Context) {
        projectDir = File(context.filesDir, "天工开物")
        if (!projectDir.exists()) projectDir.mkdirs()
    }

    fun getProjects(): List<File> = projectDir.listFiles { f -> f.extension == "pinyin" }?.toList() ?: emptyList()
    fun createProject(name: String): File = File(projectDir, name).apply { createNewFile() }
    fun readProject(file: File) = file.readText()
    fun saveProject(file: File, content: String) = file.writeText(content)
}
