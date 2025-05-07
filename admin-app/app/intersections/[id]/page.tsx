import React from "react";
import { MapIcon } from "lucide-react";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Separator } from "@/components/ui/separator";
import { intersectionApi } from "@/lib/api";
import { IntersectionForm } from "./intersection-form";

// Generate static params for the dynamic route
export async function generateStaticParams() {
  const response = await intersectionApi.getAll();
  const intersections = response.result || [];
  
  return intersections.map((intersection) => ({
    id: intersection.id.toString(),
  }));
}

export default function IntersectionPage({ params }: { params: { id: string } }) {
  const isNew = params.id === "new";
  const title = isNew ? "Create Intersection" : `Edit Intersection ${params.id}`;

  return (
    <div className="container mx-auto py-8">
      <Card className="max-w-4xl mx-auto">
        <CardHeader>
          <div className="flex items-center gap-2">
            <MapIcon className="h-6 w-6 text-primary" />
            <CardTitle>{title}</CardTitle>
          </div>
        </CardHeader>
        <Separator />
        <CardContent className="pt-6">
          <IntersectionForm id={isNew ? null : parseInt(params.id)} />
        </CardContent>
      </Card>
    </div>
  );
}