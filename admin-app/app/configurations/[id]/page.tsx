import React from "react";
import { Settings } from "lucide-react";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Separator } from "@/components/ui/separator";
import { configurationApi } from "@/lib/api";
import { ConfigurationForm } from "./configuration-form";

export async function generateStaticParams() {
  const response = await configurationApi.getAll();
  const configurations = response.result || [];
  
  return configurations.map((config) => ({
    id: config.id.toString(),
  }));
}

export default function ConfigurationPage({ params }: { params: { id: string } }) {
  const isNew = params.id === "new";
  const title = isNew ? "Create Configuration" : `Edit Configuration ${params.id}`;

  return (
    <div className="container mx-auto py-8">
      <Card className="max-w-4xl mx-auto">
        <CardHeader>
          <div className="flex items-center gap-2">
            <Settings className="h-6 w-6 text-primary" />
            <CardTitle>{title}</CardTitle>
          </div>
        </CardHeader>
        <Separator />
        <CardContent className="pt-6">
          <ConfigurationForm id={isNew ? null : parseInt(params.id)} />
        </CardContent>
      </Card>
    </div>
  );
}